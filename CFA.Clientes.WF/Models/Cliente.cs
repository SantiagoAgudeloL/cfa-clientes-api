namespace CFA.Clientes.WF.Models
{
    public class Cliente
    {
        public int Codigo { get; set; }

        public string NombreCompleto { get; set; } = "";

        public long NumeroDocumento { get; set; }

        public DateTime FechaNacimiento { get; set; }
    }
}
