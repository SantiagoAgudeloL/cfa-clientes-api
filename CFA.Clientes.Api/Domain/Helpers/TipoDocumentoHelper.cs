using CFA.Clientes.Api.Domain.Enums;

namespace CFA.Clientes.Api.Domain.Helpers
{
    public static class TipoDocumentoHelper
    {
        public static bool EsValidoParaEdad(TipoDocumento tipo, int edad)
        {
            if (edad <= 7)
                return tipo == TipoDocumento.RC;

            if (edad >= 8 && edad <= 17)
                return tipo == TipoDocumento.TI;

            return tipo == TipoDocumento.CC;
        }
    }
}
