using MastermindChallenge.API.Data;
using Microsoft.AspNetCore.Identity;

namespace MastermindChallenge.API.Utilities
{
    public class SeedUtility
    {
        PasswordHasher<Player> hasher = new PasswordHasher<Player>();

        //public static IEnumerable<T> SeedData<T>() where T : class
        //{
        //    var players = new List<T>();
        //    var player = new Player
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        UserName
        //    }
        //}

        public List<Player> SeedPlayersList()
        {
            List<Player> players = [
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
            ];

            return players;
        }
    }
}
