using Caso1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Caso1.Data;

public partial class Caso1Context : IdentityDbContext<ApplicationUser>
{
    public Caso1Context(DbContextOptions<Caso1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Habitacion> Habitaciones { get; set; }

    public virtual DbSet<Reservacion> Reservaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("HABITACIONES");

            entity.Property(e => e.CodigoDeHabitacion).HasColumnType("varchar(7)").IsRequired();
            entity.Property(e => e.NombreDeHabitacion).HasColumnType("varchar(30)").IsRequired();
            entity.Property(e => e.Ubicacion).HasColumnType("varchar(10)").IsRequired();
            entity.Property(e => e.EncargadoDeLimpieza).HasColumnType("varchar(100)").IsRequired();

            entity.Property(e => e.CostoDeLimpieza).HasColumnType("decimal(18, 2)").IsRequired();
            entity.Property(e => e.CostoDeReserva).HasColumnType("decimal(18, 2)").IsRequired();

            entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.FechaDeModificacion).HasColumnType("datetime").IsRequired(false);
        });

        modelBuilder.Entity<Reservacion>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("RESERVACIONES");

            entity.Property(e => e.NombreDeLaPersona).HasColumnType("varchar(150)").IsRequired();
            entity.Property(e => e.Identificacion).HasColumnType("varchar(30)").IsRequired();
            entity.Property(e => e.Telefono).HasColumnType("varchar(10)").IsRequired();
            entity.Property(e => e.Correo).HasColumnType("varchar(50)").IsRequired();
            entity.Property(e => e.Direccion).HasColumnType("varchar(200)").IsRequired();
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 2)").IsRequired();
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.FechaInicioReserva).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.FechaFinReserva).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.CantidadDePersonas).IsRequired();
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}