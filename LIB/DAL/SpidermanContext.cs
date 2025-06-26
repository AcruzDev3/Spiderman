using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LIB.DAL;

public partial class SpidermanContext : DbContext
{
    public SpidermanContext()
    {
    }

    public SpidermanContext(DbContextOptions<SpidermanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Crime> Crimes { get; set; }

    public virtual DbSet<HeroCrime> HeroCrimes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(1);

            entity.ToTable("Address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Side)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("side");
            entity.Property(e => e.Street)
                .HasMaxLength(150)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("zipCode");
        });

        modelBuilder.Entity<Crime>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(1);

            entity.ToTable("Crime");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.End)
                .HasColumnType("datetime")
                .HasColumnName("end");
            entity.Property(e => e.Grade)
                .HasMaxLength(20)
                .HasColumnName("grade");
            entity.Property(e => e.IdAddress).HasColumnName("idAddress");
            entity.Property(e => e.Start)
                .HasColumnType("datetime")
                .HasColumnName("start");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.IdAddressNavigation).WithMany(p => p.Crimes)
                .HasForeignKey(d => d.IdAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Crime_Address");
        });

        modelBuilder.Entity<HeroCrime>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(1);

            entity.ToTable("heroCrime");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCrime).HasColumnName("idCrime");
            entity.Property(e => e.IdHero).HasColumnName("idHero");

            entity.HasOne(d => d.IdCrimeNavigation).WithMany(p => p.HeroCrimes)
                .HasForeignKey(d => d.IdCrime)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_heroCrime_Crime");

            entity.HasOne(d => d.IdHeroNavigation).WithMany(p => p.HeroCrimes)
                .HasForeignKey(d => d.IdHero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_heroCrime_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(1);

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "IX_User").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .HasColumnName("role");
            entity.Property(e => e.Salt)
                .HasMaxLength(100)
                .HasColumnName("salt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
