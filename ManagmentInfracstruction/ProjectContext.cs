using System;
using System.Collections.Generic;
using Npgsql;
using ProjectManagment_class.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagmentInfracstruction;

public partial class ProjectContext : DbContext
{
    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }
   
    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ProjectManagment_class.Models.Task> Tasks { get; set; }

    public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
#warning


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
    .Property(p => p.ProjectId)
    .HasColumnName("projectid") // Убедитесь, что в базе имя маленькими буквами
    .UseIdentityByDefaultColumn();



        modelBuilder.Entity<Project>(entity =>
        {
            entity.Ignore(e => e.ProjectId);
            entity.HasKey(e => e.ProjectId).HasName("Projects_pkey");

            entity.Property(e => e.ProjectId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Project_ID");
            entity.Property(e => e.DateEnd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_end");
            entity.Property(e => e.DateStart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_start");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ManagerId).HasColumnName("Manager_ID");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(200)
                .HasColumnName("project_name");

            entity.HasOne(d => d.Manager).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_manager");
        });

        modelBuilder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(e => e.ProjectMembersId).HasName("ProjectMembers_pkey");

            entity.Property(e => e.ProjectMembersId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ProjectMembers_ID");
            entity.Property(e => e.JoinDate).HasColumnName("join_date");
            entity.Property(e => e.ProjectId).HasColumnName("Project_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("Reports_pkey");

            entity.Property(e => e.ReportId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Report_ID");
            entity.Property(e => e.AssignmentId).HasColumnName("Assignment_ID");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Reports)
                .HasForeignKey(d => d.AssignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_assignment");
        });

        modelBuilder.Entity<ProjectManagment_class.Models.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("Tasks_pkey");

            entity.Property(e => e.TaskId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Task_ID");
            entity.Property(e => e.Deadline)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.ProjectId).HasColumnName("Project_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project");
        });

        modelBuilder.Entity<TaskAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("TaskAssignments_pkey");

            entity.Property(e => e.AssignmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Assignment_ID");
            entity.Property(e => e.IsLead).HasColumnName("is_lead");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task");

            entity.HasOne(d => d.User).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<User>(entity =>
        {

            entity.HasKey(e => e.UserId).HasName("Users_pkey");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("User_ID");
            entity.Property(e => e.Bio).HasColumnName("BIO");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
