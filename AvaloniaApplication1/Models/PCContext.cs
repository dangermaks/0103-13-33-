﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1.Models;

public partial class PCContext : DbContext
{
    public PCContext()
    {
    }

    public PCContext(DbContextOptions<PCContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tovar> Tovars { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(LOCALDB)\\MSSQLLOCALDB;Initial Catalog=PC;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tovar>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Cost)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Kolichestvo)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Opisanie)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}