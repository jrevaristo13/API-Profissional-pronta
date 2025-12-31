using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Domain; // Esta linha é essencial aqui!

namespace SistemaPedidos.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       
        public DbSet<Usuario> Usuarios { get; set; }
    }
}