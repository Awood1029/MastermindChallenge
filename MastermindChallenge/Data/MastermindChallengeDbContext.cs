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
            //entity.HasMany(p => p.Games)
            //.WithOne(p => p.Player);

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
                UserName = "testuser1",
                FirstName = "Seed",
                LastName = "Data",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new Player
            {
                Id = "9321fd15-7f7d-42b9-9f45-ba63eec8e593",
                UserName = "testuser2",
                FirstName = "Data",
                LastName = "Seed",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new Player
            {
                Id = "c3b7e9e9-1da7-4b11-a3bf-3871da977b7e",
                UserName = "testuser3",
                FirstName = "Test",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new Player
            {
                Id = "e64eef9d-6c4d-4af9-8ace-60b96825c05a",
                UserName = "testuser4",
                FirstName = "Test",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new Player
            {
                Id = "a13f7650-1f8f-4401-8e80-40be41637141",
                UserName = "testuser5",
                FirstName = "Test",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new Player
            {
                Id = "67b06a1c-f6ad-4527-b2d5-0eb3917e739e",
                UserName = "testuser6",
                FirstName = "Test",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new Player
            {
                Id = "4ae472f3-08ad-42d6-a593-1e6eae9b8332",
                UserName = "testuser7",
                FirstName = "Test",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            }
        );

        //modelBuilder.Entity<Game>().HasData(
        //    new Game
        //    {
        //        Id = 1,
        //        Difficulty = 4,
        //        IsWinner = true,
        //        AttemptsUsed = 1,
        //        PlayerId = "e9b3c7c3-1da7-4b11-a3bf-3871da977b7e",
        //        AnswerToGuess = [1, 2, 3, 4]
        //    },
        //    new Game
        //    {
        //        Id = 2,
        //        Difficulty = 4,
        //        IsWinner = true,
        //        AttemptsUsed = 2,
        //        PlayerId = "9321fd15-7f7d-42b9-9f45-ba63eec8e593",
        //        AnswerToGuess = [1, 2, 3, 4]
        //    },
        //    new Game
        //    {
        //        Id = 3,
        //        Difficulty = 4,
        //        IsWinner = true,
        //        AttemptsUsed = 3,
        //        PlayerId = "c3b7e9e9-1da7-4b11-a3bf-3871da977b7e",
        //        AnswerToGuess = [1,2,3,4]
        //    },
        //    new Game
        //    {
        //        Id = 4,
        //        Difficulty = 4,
        //        IsWinner = true,
        //        AttemptsUsed = 4,
        //        PlayerId = "e64eef9d-6c4d-4af9-8ace-60b96825c05a",
        //        AnswerToGuess = [1,2,3,4]
        //    },
        //    new Game
        //    {
        //        Id = 5,
        //        Difficulty = 4,
        //        IsWinner = true,
        //        AttemptsUsed = 5,
        //        PlayerId = "a13f7650-1f8f-4401-8e80-40be41637141",
        //        AnswerToGuess = [1,2,3,4]
        //    },
        //    new Game
        //    {
        //        Id = 6,
        //        Difficulty = 4,
        //        IsWinner = true,
        //        AttemptsUsed = 6,
        //        PlayerId = "67b06a1c-f6ad-4527-b2d5-0eb3917e739e",
        //        AnswerToGuess = [1,2,3,4]
        //    },
        //    new Game
        //    {
        //        Id = 7,
        //        Difficulty = 4,
        //        IsWinner = true,
        //        AttemptsUsed = 7,
        //        PlayerId = "4ae472f3-08ad-42d6-a593-1e6eae9b8332",
        //        AnswerToGuess = [1,2,3,4]}
        //    );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
