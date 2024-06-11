using ExampleTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Character_title> CharacterTitles { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
            {
                new Backpack
                {
                    CharacterId = 1,
                    ItemId = 1,
                    Amount = 20
                },
                new Backpack
                {
                    CharacterId = 2,
                    ItemId = 2,
                    Amount = 30
                },
                new Backpack
                {
                    CharacterId = 3,
                    ItemId = 3,
                    Amount = 40
                },
            });

            modelBuilder.Entity<Character>().HasData(new List<Character>
            {
                new Character {
                    Id = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    CurrentWeight = 20,
                    MaxWeight = 50
                },
                new Character {
                    Id = 2,
                    FirstName = "Janina",
                    LastName = "Kowalska",
                    CurrentWeight = 15,
                    MaxWeight = 80
                }, new Character {
                    Id = 3,
                    FirstName = "Joana",
                    LastName = "Kowalska",
                    CurrentWeight = 30,
                    MaxWeight = 90
                },
            });

            modelBuilder.Entity<Title>().HasData(new List<Title>
            {
                new Title
                {
                    Id = 1,
                    Name = "Kozak"
                },
                new Title
                {
                    Id = 2,
                    Name = "Kocur"
                },
                new Title
                {
                    Id = 3,
                    Name = "Champion"
                }
            });

            modelBuilder.Entity<Character_title>().HasData(new List<Character_title>
            {
                new Character_title
                {
                   CharacterId = 1,
                   TitleId = 1,
                   AcquiredAt = DateTime.MinValue
                },
                new Character_title
                {
                    CharacterId = 2,
                    TitleId = 2,
                    AcquiredAt = DateTime.MaxValue
                },
                new Character_title
                {
                    CharacterId = 3,
                    TitleId = 3,
                    AcquiredAt = DateTime.Now
                },
            });

            modelBuilder.Entity<Item>().HasData(new List<Item>
            {
                new Item
                {
                   Id = 1,
                   Name = "Telefon",
                   Weight = 10

                },
                new Item
                {
                    Id = 2,
                    Name = "Klucze",
                    Weight = 5

                },
                new Item
                {
                    Id = 3,
                    Name = "Woda",
                    Weight = 15

                },
            });
    }
}
