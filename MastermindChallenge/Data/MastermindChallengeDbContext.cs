using System;
using System.Collections.Generic;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MastermindChallenge.API.Data;

public partial class MastermindChallengeDbContext : IdentityDbContext
{
    public MastermindChallengeDbContext()
    {
    }

    public MastermindChallengeDbContext(DbContextOptions<MastermindChallengeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Games__3214EC07B8CE5631");

            entity.Property(e => e.Difficulty).HasDefaultValue(1);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        var hasher = new PasswordHasher<Player>();

        modelBuilder.Entity<Player>().HasData(
            new Player
            {
                Id = "e9b3c7c3-1da7-4b11-a3bf-3871da977b7e",
                Email = "seeddata@email.com",
                UserName = "seeddata101",
                FirstName = "Seed",
                LastName = "Data",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new Player
            {
                Id = "9321fd15-7f7d-42b9-9f45-ba63eec8e593",
                Email = "dataseed@email.com",
                UserName = "dataseed101",
                FirstName = "Data",
                LastName = "Seed",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            }
    );
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
