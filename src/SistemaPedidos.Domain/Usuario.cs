namespace SistemaPedidos.Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = ""; // Trocamos Preco por Email
        public string Cargo { get; set; } = ""; // Trocamos Estoque por Cargo
    }
}