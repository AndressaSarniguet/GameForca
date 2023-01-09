using GameForca.Models;
using Microsoft.EntityFrameworkCore;

namespace GameForca.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; } = null!;

        public DbSet<PalavraSecreta> PalavrasSecretas { get; set; } = null!;
    }
}
