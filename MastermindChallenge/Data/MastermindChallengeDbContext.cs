using System;
using System.Collections.Generic;
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
