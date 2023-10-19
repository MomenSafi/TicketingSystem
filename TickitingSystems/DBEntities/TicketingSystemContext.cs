﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TickitingSystems.DBEntities
{
    public partial class TicketingSystemContext : DbContext
    {
        public TicketingSystemContext()
        {
        }

        public TicketingSystemContext(DbContextOptions<TicketingSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IssueDetail> IssueDetails { get; set; }
        public virtual DbSet<IssueDetailsLog> IssueDetailsLogs { get; set; }
        public virtual DbSet<IssueStatus> IssueStatuses { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Serverity> Serverities { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=momensafi\\momensafi; Database=TicketingSystem;Trusted_Connection=True; User Id=sa; password=123456; Integrated Security=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssueDetail>(entity =>
            {
                entity.HasKey(e => e.IssueDetailsId);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EnviromentType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IssueDescription).IsRequired();

                entity.Property(e => e.ResolvedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IssueStatus)
                    .WithMany(p => p.IssueDetails)
                    .HasForeignKey(d => d.IssueStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueDetails_IssueStatus");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.IssueDetails)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueDetails_Project");

                entity.HasOne(d => d.Serverity)
                    .WithMany(p => p.IssueDetails)
                    .HasForeignKey(d => d.ServerityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueDetails_Serverity");
            });

            modelBuilder.Entity<IssueDetailsLog>(entity =>
            {
                entity.ToTable("IssueDetailsLog");

                entity.Property(e => e.ActionDate).HasColumnType("datetime");

                entity.HasOne(d => d.IssueDetails)
                    .WithMany(p => p.IssueDetailsLogs)
                    .HasForeignKey(d => d.IssueDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueDetailsLog_IssueDetails");

                entity.HasOne(d => d.IssueStatus)
                    .WithMany(p => p.IssueDetailsLogs)
                    .HasForeignKey(d => d.IssueStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueDetailsLog_IssueStatus");
            });

            modelBuilder.Entity<IssueStatus>(entity =>
            {
                entity.HasKey(e => e.IssueStatesId)
                    .HasName("PK_IssueStates");

                entity.ToTable("IssueStatus");

                entity.Property(e => e.IssueStatesName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactPerson)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DeliverDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Serverity>(entity =>
            {
                entity.ToTable("Serverity");

                entity.Property(e => e.ServerityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
