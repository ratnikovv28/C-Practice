using Microsoft.EntityFrameworkCore;
using Practice_7.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Practice_7
{
    public class SongsDBContext : DbContext
    {
        public SongsDBContext() : this(false) { }

        public SongsDBContext(bool bFromScratch) : base()
        {
            if (bFromScratch)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public SongsDBContext(DbContextOptions<SongsDBContext> options)
            : base(options)
        {
        }

        // Коллекции сущностей
        public DbSet<Song> Songs { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=mssql-125241-0.cloudclusters.net,10013;User ID=Main;Password=Main1234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Song>().Property(a => a.Name).IsRequired();
        //    modelBuilder.Entity<Song>().HasKey(a => a.Id);

        //    modelBuilder.Entity<Author>().Property(m => m.Pseudonym).IsRequired();
        //    modelBuilder.Entity<Author>().HasKey(m => m.Id);

        //    modelBuilder.Entity<Song>().HasOne<Author>(d => d.Author)
        //    .WithMany(a => a.Songs).HasForeignKey(d => d.AuthorId);


        //    modelBuilder.Entity<Author>().HasData(
        //    new Author[]
        //    {
        //        new Author {Id = 1, Pseudonym = "Lil Baby", Country = "USA"},
        //        new Author {Id = 2, Pseudonym = "Central Cee", Country = "England"},
        //        new Author {Id = 3, Pseudonym = "Travis Scott", Country = "USA"},
        //        new Author {Id = 4, Pseudonym = "ZAZ", Country = "France"},
        //        new Author {Id = 5, Pseudonym = "Obladaet", Country = "Russia"}
        //    });

        //    modelBuilder.Entity<Song>().HasData(
        //    new Song[]
        //    {
        //        new Song {Id = 1, Name = "Drip Too Hard", Genre = "Rap", Year = 2018, AuthorId = 1},
        //        new Song {Id = 2, Name = "Yes Indeed", Genre = "Rap", Year = 2018, AuthorId = 1},
        //        new Song {Id = 3, Name = "Je veux", Genre = "Jazz", Year = 2010, AuthorId = 4},
        //        new Song {Id = 4, Name = "For Mula", Genre = "Rap", Year = 2021, AuthorId = 5},
        //        new Song {Id = 5, Name = "Doja", Genre = "Rap", Year = 2022, AuthorId = 2},
        //        new Song {Id = 6, Name = "SICKO MODE", Genre = "Rap", Year = 2018, AuthorId = 3},
        //    });
        //}
    }
}
