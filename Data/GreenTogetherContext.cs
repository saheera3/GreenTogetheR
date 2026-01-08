using System;
using System.Collections.Generic;
using GreenTogetheR.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenTogetheR.Data;

public partial class GreenTogetherContext : DbContext
{
    public GreenTogetherContext()
    {
    }

    public GreenTogetherContext(DbContextOptions<GreenTogetherContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AwarenessArticles> AwarenessArticles { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<IllegalDumpReport> IllegalDumpReports { get; set; }

    public virtual DbSet<PointEarned> PointEarneds { get; set; }

    public virtual DbSet<RecylingCenter> RecylingCenters { get; set; }

    public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }

    public virtual DbSet<WasteSchedule> WasteSchedules { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=SAHEERA3\\SQLEXPRESS;Initial Catalog=GreenTogether;Integrated Security=True;Trust Server Certificate=True;");
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AwarenessArticles>(entity =>
        {
            entity.HasKey(e => e.ArticleId);

            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.ArticleDescription)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ArticleTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FileUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FileURL");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK_city");

            entity.ToTable("City");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IllegalDumpReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK_illegalDumpReport");

            entity.ToTable("IllegalDumpReport");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PhotoURL");
            entity.Property(e => e.ReportDate).HasColumnType("datetime");
            entity.Property(e => e.ReporterName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.City).WithMany(p => p.IllegalDumpReports)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_illegalDumpReport_city");

            entity.HasOne(d => d.User).WithMany(p => p.IllegalDumpReports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_illegalDumpReport_RegisteredUser");
        });

        modelBuilder.Entity<PointEarned>(entity =>
        {
            entity.HasKey(e => e.PointId);

            entity.ToTable("pointEarned");

            entity.Property(e => e.PointId).HasColumnName("pointID");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateEarned).HasColumnType("datetime");
            entity.Property(e => e.PointsEarned).HasColumnName("pointsEarned");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.PointEarneds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pointEarned_RegisteredUser");
        });

        modelBuilder.Entity<RecylingCenter>(entity =>
        {
            entity.HasKey(e => e.CenterId);

            entity.ToTable("recylingCenter");

            entity.Property(e => e.CenterId).HasColumnName("CenterID");
            entity.Property(e => e.CenterAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("centerAddress");
            entity.Property(e => e.CenterName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("centerName");
            entity.Property(e => e.CityId).HasColumnName("cityID");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contactInfo");

            entity.HasOne(d => d.City).WithMany(p => p.RecylingCenters)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recylingCenter_city");
        });

        modelBuilder.Entity<RegisteredUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("RegisteredUser");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.RegisteredUsers)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisteredUser_city");
        });

        modelBuilder.Entity<WasteSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK_wasteSchedule");

            entity.ToTable("WasteSchedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CollectionDay)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WasteType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.WasteSchedules)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wasteSchedule_city");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
