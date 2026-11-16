using Hangfire;
using Hangfire.SqlServer;
using OfficeOpenXml;
using QuestPDF.Infrastructure;
using Systemia.Datos;

// Licencias (obligatorias; si no se configuran, EPPlus y QuestPDF lanzan excepción al usarlos).
// EPPlus 8: uso no comercial gratis; para uso comercial: ExcelPackage.License.SetCommercial("<clave>").
ExcelPackage.License.SetNonCommercialOrganization("Systemia");
// QuestPDF: Community es gratis para empresas con ingresos anuales < 1M USD.
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Hangfire: usa la misma conexión de ContextoDB (crea sus tablas en el esquema [HangFire] al iniciar).
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(ContextoDB.CrearConexion, new SqlServerStorageOptions()));

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

// Panel de Hangfire en /hangfire (por defecto solo accesible desde localhost).
app.UseHangfireDashboard("/hangfire");

app.MapControllers();

app.Run();
