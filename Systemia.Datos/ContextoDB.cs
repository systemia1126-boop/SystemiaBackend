using Dapper;
using Microsoft.Data.SqlClient;
using Systemia.Entidades;
using System.Data;

namespace Systemia.Datos
{
    public static class ContextoDB
    {
        //public static string cadenaConexion = @"Data Source=.\SQLEXPRESS; Initial Catalog=OneZip1; uid=innovasoft; password=ingeniero91@; TrustServerCertificate=True";
        public static string cadenaConexion = @"Data Source=YOHANY\INSTANCIA; Initial Catalog=Systemia; Trusted_Connection=True; TrustServerCertificate=True";

        private const int TiempoEspera = 600;
        public static SqlConnection CrearConexion() => new SqlConnection(cadenaConexion);
        public static async Task<SqlConnection> TraerConexionDBAsync(CancellationToken cancellationToken = default)
        {
            var conexion = CrearConexion();
            try
            {
                await conexion.OpenAsync(cancellationToken);
                return conexion;
            }
            catch (Exception ex)
            {
                await conexion.DisposeAsync();
                throw new ExceptionPropia("Fallo al abrir conexión", ex);
            }
        }
        public static async Task InsertarAsync(List<SQL> listaSQL, CancellationToken cancellationToken = default)
        {
            await InsertarRetornarAsync(listaSQL, cancellationToken);
        }
        public static async Task<Dictionary<string, int>> InsertarRetornarAsync(List<SQL> listaSQL, CancellationToken cancellationToken = default)
        {
            var datos = new Dictionary<string, int>();

            await using var db = await TraerConexionDBAsync(cancellationToken);
            await using var transaccion = await db.BeginTransactionAsync(IsolationLevel.ReadUncommitted, cancellationToken);

            foreach (var item in listaSQL)
            {
                // Tipo 1 (Padre) solo anula las claves que aún no existen; el resto las reemplaza por el Id generado.
                string sql = ReemplazarClaves(item.Sql, item.Claves, datos, soloNulos: item.Tipo == 1);
                var comando = new CommandDefinition(sql, (object?)item.Valores, transaccion, TiempoEspera, cancellationToken: cancellationToken);

                switch (item.Tipo)
                {
                    case 1:
                    case 3:
                        datos["Id" + item.Tabla] = await db.ExecuteScalarAsync<int>(comando);
                        break;
                    default:
                        await db.ExecuteAsync(comando);
                        break;
                }
            }

            await transaccion.CommitAsync(cancellationToken);
            return datos;
        }
        public static async Task ActualizarAsync(List<SQL> listaSQL, CancellationToken cancellationToken = default)
        {
            await using var db = await TraerConexionDBAsync(cancellationToken);
            await using var transaccion = await db.BeginTransactionAsync(cancellationToken);

            foreach (var item in listaSQL)
            {
                await db.ExecuteAsync(new CommandDefinition(item.Sql, (object?)item.Valores, transaccion, TiempoEspera, cancellationToken: cancellationToken));
            }

            await transaccion.CommitAsync(cancellationToken);
        }
        private static string ReemplazarClaves(string sql, List<string> claves, Dictionary<string, int> datos, bool soloNulos)
        {
            foreach (var clave in claves)
            {
                if (datos.TryGetValue(clave, out int dato))
                {
                    if (!soloNulos)
                        sql = sql.Replace("@" + clave, dato.ToString());
                }
                else
                {
                    sql = sql.Replace("@" + clave, "null");
                }
            }
            return sql;
        }
    }
}
