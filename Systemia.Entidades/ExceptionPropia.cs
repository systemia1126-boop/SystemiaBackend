namespace Systemia.Entidades
{
    public class ExceptionPropia : Exception
    {
        public ExceptionPropia(string mensaje) : base(mensaje) { }

        public ExceptionPropia(string mensaje, Exception interna) : base(mensaje, interna) { }
    }
}
