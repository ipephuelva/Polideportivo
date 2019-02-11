using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetCore.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Deportes> Deportes { get; set; }
        public virtual DbSet<Pistas> Pistas { get; set; }
        public virtual DbSet<Reservas> Reservas { get; set; }
        public virtual DbSet<Socios> Socios { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-S5GEQMP6;Initial Catalog=Polideportivo_test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deportes>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Deportes")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Pistas>(entity =>
            {
                entity.HasIndex(e => new { e.Sport, e.NField })
                    .HasName("IX_Pistas")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NField).HasColumnName("n_field");

                entity.Property(e => e.Sport)
                    .IsRequired()
                    .HasColumnName("sport")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Reservas>(entity =>
            {
                entity.HasIndex(e => new { e.Sport, e.NField, e.Date })
                    .HasName("IX_Reservas")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasColumnName("dni")
                    .HasMaxLength(9);

                entity.Property(e => e.NField).HasColumnName("n_field");

                entity.Property(e => e.Sport)
                    .IsRequired()
                    .HasColumnName("sport")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Socios>(entity =>
            {
                entity.HasIndex(e => e.Dni)
                    .HasName("IX_Socios")
                    .IsUnique();

                entity.HasIndex(e => e.User)
                    .HasName("IX_Socios_1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasColumnName("dni")
                    .HasMaxLength(9);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(15);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);

                entity.Property(e => e.Telephone).HasColumnName("telephone");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasColumnName("user")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasIndex(e => e.User)
                    .HasName("IX_Usuarios")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(15);

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasColumnName("user")
                    .HasMaxLength(15);
            });
        }
    }
}
