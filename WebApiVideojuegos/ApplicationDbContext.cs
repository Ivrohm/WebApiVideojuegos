using Microsoft.EntityFrameworkCore;
using WebApiVideojuegos.Entidades;

namespace WebApiVideojuegos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Videojuego> Videojuegos { set; get; }

        public DbSet<EspecVideojuego> EspecVideojuegos { get; set; }
    }
}
