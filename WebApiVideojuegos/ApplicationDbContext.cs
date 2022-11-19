using Microsoft.EntityFrameworkCore;
using WebApiVideojuegos.Entidades;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiVideojuegos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideojuegoTiendaVideojuego>()
                .HasKey(al => new { al.VideojuegoId, al.tiendaVideojuegoId });
        }

        public DbSet<Videojuego> Videojuegos { set; get; }
       
        public DbSet<TiendaVideojuego> TiendaVideojuegos { get; set; }
     
        public DbSet<Reseña> Reseñas { get; set; }
   
        public DbSet<VideojuegoTiendaVideojuego> VideojuegoTiendaVideojuego { get; set; }
    }
}
