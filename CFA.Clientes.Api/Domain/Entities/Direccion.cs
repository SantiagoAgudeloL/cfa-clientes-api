namespace CFA.Clientes.Api.Domain.Entities
{
    public class Direccion
    {
        public int Id { get; set; }

        public int ClienteCodigo { get; set; }

        public string Descripcion { get; set; } = string.Empty;
    
}
}
