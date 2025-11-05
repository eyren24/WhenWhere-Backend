using System;
using System.Collections.Generic;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agenda> Agenda { get; set; }

    public virtual DbSet<Evento> Evento { get; set; }

    public virtual DbSet<Likes> Likes { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    public virtual DbSet<RefreshToken> RefreshToken { get; set; }

    public virtual DbSet<Ruoli> Ruoli { get; set; }

    public virtual DbSet<Tag> Tag { get; set; }

    public virtual DbSet<Utente> Utente { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agenda>(entity =>
        {
            entity.Property(e => e.isprivate).HasDefaultValue(true);

            entity.HasOne(d => d.utente).WithMany(p => p.Agenda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agenda_Utente");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.Property(e => e.dataCreazione).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.agenda).WithMany(p => p.Evento).HasConstraintName("FK_Evento_Agenda");

            entity.HasOne(d => d.tag).WithMany(p => p.Evento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evento_Tag");
        });

        modelBuilder.Entity<Likes>(entity =>
        {
            entity.HasOne(d => d.agenda).WithMany(p => p.Likes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_Agenda");

            entity.HasOne(d => d.utente).WithMany(p => p.Likes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_Utente");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.Property(e => e.dataCreazione).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.agenda).WithMany(p => p.Nota).HasConstraintName("FK_Nota_Agenda");

            entity.HasOne(d => d.tag).WithMany(p => p.Nota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nota_Tag");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasOne(d => d.utente).WithMany(p => p.RefreshToken)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_Utente");
        });

        modelBuilder.Entity<Utente>(entity =>
        {
            entity.Property(e => e.dataCreazione).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.fotoProfilo).HasDefaultValue("default.png");
            entity.Property(e => e.lastLogin).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.preferenzeNotifiche).HasDefaultValue(true);
            entity.Property(e => e.ruoloId).HasDefaultValue(2);
            entity.Property(e => e.statoAccount).HasDefaultValue(true);
            entity.Property(e => e.token).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}