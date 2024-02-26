using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EYProveedores.Models;

public partial class EyproveedoresContext : DbContext
{
    public EyproveedoresContext()
    {
    }

    public EyproveedoresContext(DbContextOptions<EyproveedoresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ListaScreening> ListaScreenings { get; set; }

    public virtual DbSet<Offacsource> Offacsources { get; set; }

    public virtual DbSet<OffshoreSource> OffshoreSources { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TheWorldBankSource> TheWorldBankSources { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local); DataBase=EYProveedores; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__FCE03992C2085C33");

            entity.ToTable("Cliente", "EYProveedores");

            entity.Property(e => e.IdCliente).HasColumnName("Id_cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("cliente_users");
        });

        modelBuilder.Entity<ListaScreening>(entity =>
        {
            entity.HasKey(e => e.IdScreening).HasName("Lista_screening_pk");

            entity.ToTable("Lista_screening", "EYProveedores");

            entity.Property(e => e.IdScreening)
                .ValueGeneratedNever()
                .HasColumnName("Id_screening");
            entity.Property(e => e.IdCliente).HasColumnName("Id_cliente");
            entity.Property(e => e.IdOfac).HasColumnName("Id_ofac");
            entity.Property(e => e.IdOs).HasColumnName("Id_os");
            entity.Property(e => e.IdProveedor).HasColumnName("Id_proveedor");
            entity.Property(e => e.IdWbs).HasColumnName("Id_wbs");
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ListaScreenings)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lista_screening_cliente");

            entity.HasOne(d => d.IdOfacNavigation).WithMany(p => p.ListaScreenings)
                .HasForeignKey(d => d.IdOfac)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lista_screening_OFFACSource");

            entity.HasOne(d => d.IdOsNavigation).WithMany(p => p.ListaScreenings)
                .HasForeignKey(d => d.IdOs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lista_screening_OffshoreSource");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ListaScreenings)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lista_screening_proveedor");

            entity.HasOne(d => d.IdWbsNavigation).WithMany(p => p.ListaScreenings)
                .HasForeignKey(d => d.IdWbs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lista_screening_TheWorldBankSource");
        });

        modelBuilder.Entity<Offacsource>(entity =>
        {
            entity.HasKey(e => e.IdOfac).HasName("OFACSource_pk");

            entity.ToTable("OFFACSource", "EYProveedores");

            entity.Property(e => e.IdOfac)
                .ValueGeneratedNever()
                .HasColumnName("Id_ofac");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.List)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Programs)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Score)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OffshoreSource>(entity =>
        {
            entity.HasKey(e => e.IdOs).HasName("OffshoreSource_pk");

            entity.ToTable("OffshoreSource", "EYProveedores");

            entity.Property(e => e.IdOs)
                .ValueGeneratedNever()
                .HasColumnName("Id_os");
            entity.Property(e => e.DataFrom)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("data_from");
            entity.Property(e => e.Entity)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Jurisdiction)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkedTo)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Linked_to");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("Proveedor_pk");

            entity.ToTable("Proveedor", "EYProveedores");

            entity.Property(e => e.IdProveedor)
                .ValueGeneratedNever()
                .HasColumnName("Id_proveedor");
            entity.Property(e => e.Correo)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Facturacion).HasColumnType("money");
            entity.Property(e => e.IdTributaria).HasColumnName("Id_tributaria");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Razon_social");
            entity.Property(e => e.SitioWeb)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Sitio_web");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("Roles_pk");

            entity.ToTable("Roles", "EYProveedores");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("Id_rol");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.Rol)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roles_users");
        });

        modelBuilder.Entity<TheWorldBankSource>(entity =>
        {
            entity.HasKey(e => e.IdWbs).HasName("TheWorldBankSource_pk");

            entity.ToTable("TheWorldBankSource", "EYProveedores");

            entity.Property(e => e.IdWbs)
                .ValueGeneratedNever()
                .HasColumnName("id_wbs");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirmName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Firm_name");
            entity.Property(e => e.FromDate).HasColumnName("From_date");
            entity.Property(e => e.Grounds)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ToDate).HasColumnName("To_date");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Users_pk");

            entity.ToTable("Users", "EYProveedores");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("Id_user");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
