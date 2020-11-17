using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace tarea6y7.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Treasure> Treasures { get; set; }
        public DbSet<Coordinate> Coordinates { get; protected set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>(entityTypeBuilder => {
                entityTypeBuilder.ToTable("Users");
                entityTypeBuilder.Property(u => u.Uid).HasMaxLength(100).IsRequired();
                entityTypeBuilder.Property(u => u.Nombre).HasMaxLength(100);
                entityTypeBuilder.Property(u => u.Color).HasMaxLength(8).HasDefaultValue("#ffffff");
            });
        }
    }

    public class AppUser : IdentityUser
    {
        public Guid Uid { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

    public class Treasure
    {
        public Guid Uid { get; set; }
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public float Peso { get; set; }
        public string Lugar { get; set; }
        public float Valor { get; set; }
        public Coordinate Coordenadas { get; set; }
        public string UrlRef { get; set; }
    }

    public class Coordinate
    {
        public Guid Id { get; set; }
        public Guid Tid { get; set; }
        public Guid Uid { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
    }
}
