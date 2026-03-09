namespace CFA.Clientes.Api.Domain.Helpers
{
    public static class EdadHelper
    {
        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;

            int edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > hoy.AddYears(-edad))
                edad--;

            return edad;
        }
    }
}
