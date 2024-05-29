using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Data.Models;

namespace WizardFormBackend.Data.Context;

public partial class WizardFormDbContext : DbContext
{
    public WizardFormDbContext()
    {
    }

    public WizardFormDbContext(DbContextOptions<WizardFormDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FileDetail> FileDetails { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileDetail>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK__FileDeta__6F0F98BFFEA18A42");

            entity.Property(e => e.Checksum)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.PriorityCode).HasName("PK__Priority__3E2A4475F8723713");

            entity.ToTable("Priority");

            entity.Property(e => e.PriorityCode).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8517AE5B7C468");

            entity.Property(e => e.GuardianName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.File).WithMany(p => p.Requests)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("FK__Requests__FileId__619B8048");

            entity.HasOne(d => d.PriorityCodeNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.PriorityCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Priori__5FB337D6");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.StatusCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Status__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__UserId__5EBF139D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AD794C9DB");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleType)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusCode).HasName("PK__Status__6A7B44FDCC9DE055");

            entity.ToTable("Status");

            entity.Property(e => e.StatusCode).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C91F2AAB1");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
