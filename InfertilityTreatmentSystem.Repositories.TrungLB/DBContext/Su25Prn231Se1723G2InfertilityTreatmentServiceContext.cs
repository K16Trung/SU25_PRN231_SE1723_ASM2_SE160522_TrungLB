using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace InfertilityTreatmentSystem.Repositories.TrungLB.DBContext;

public partial class Su25Prn231Se1723G2InfertilityTreatmentServiceContext : DbContext
{
    public Su25Prn231Se1723G2InfertilityTreatmentServiceContext()
    {
    }

    public Su25Prn231Se1723G2InfertilityTreatmentServiceContext(DbContextOptions<Su25Prn231Se1723G2InfertilityTreatmentServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<BlogType> BlogTypes { get; set; }

    public virtual DbSet<ReminderTypeTrungLb> ReminderTypeTrungLbs { get; set; }

    public virtual DbSet<SystemUserAccount> SystemUserAccounts { get; set; }

    public virtual DbSet<TreatmentReminderTrungLb> TreatmentReminderTrungLbs { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__BlogPost__AA1260188455C028");

            entity.ToTable("BlogPost");

            entity.Property(e => e.AuthorName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsPublished).HasDefaultValue(false);
            entity.Property(e => e.LikeCount).HasDefaultValue(0);
            entity.Property(e => e.Summary).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.ViewCount).HasDefaultValue(0);

            entity.HasOne(d => d.BlogType).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.BlogTypeId)
                .HasConstraintName("FK__BlogPost__BlogTy__59FA5E80");
        });

        modelBuilder.Entity<BlogType>(entity =>
        {
            entity.HasKey(e => e.BlogTypeId).HasName("PK__BlogType__D9FADC26670BC25A");

            entity.ToTable("BlogType");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ReminderTypeTrungLb>(entity =>
        {
            entity.HasKey(e => e.ReminderTypeId).HasName("PK__Reminder__56776DF2E9F5417D");

            entity.ToTable("ReminderTypeTrungLB");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<SystemUserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__SystemUs__DA6C70BA7EAEEE1B");

            entity.ToTable("SystemUserAccount");

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<TreatmentReminderTrungLb>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Treatmen__01A8308785864E31");

            entity.ToTable("TreatmentReminderTrungLB");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRecurring).HasDefaultValue(false);
            entity.Property(e => e.IsSent).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.PatientName).HasMaxLength(200);
            entity.Property(e => e.RelatedDoctor).HasMaxLength(200);
            entity.Property(e => e.ReminderDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.ReminderType).WithMany(p => p.TreatmentReminderTrungLbs)
                .HasForeignKey(d => d.ReminderTypeId)
                .HasConstraintName("FK__Treatment__Remin__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
