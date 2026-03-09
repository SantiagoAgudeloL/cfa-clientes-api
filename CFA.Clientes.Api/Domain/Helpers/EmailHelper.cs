using System.Text.RegularExpressions;

namespace CFA.Clientes.Api.Domain.Helpers
{
    public static class EmailHelper
    {
        public static bool EsValido(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            return regex.IsMatch(email);
        }
    }
}
