using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Models;

namespace WizardFormBackend.Data;

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
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileDetail>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK__FileDeta__6F0F98BF3B972CA5");

            entity.Property(e => e.FileId).ValueGeneratedNever();
            entity.Property(e => e.Checksum)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.PriorityCode).HasName("PK__Priority__3E2A4475CAFE752F");

            entity.ToTable("Priority");

            entity.Property(e => e.PriorityCode).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8517AB89B2FA9");

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
                .HasConstraintName("FK__Requests__FileId__571DF1D5");

            entity.HasOne(d => d.PriorityCodeNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.PriorityCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Priori__5535A963");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.StatusCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Status__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__UserId__5441852A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A41F0EDCF");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleType)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusCode).HasName("PK__Status__6A7B44FD2E7A92CE");

            entity.ToTable("Status");

            entity.Property(e => e.StatusCode).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CE13CF369");

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
                .HasConstraintName("FK__Users__RoleId__5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
