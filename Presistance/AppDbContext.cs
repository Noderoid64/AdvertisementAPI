using AdvertisingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingApi.Presistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        // TODO refactor sequence part
        public long GetNextQueueSequenceValue()
        {
            var connection = Database.GetDbConnection();
            connection.Open();
            long result = 0;
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = PostgressSql.GetQueueSequenceVal();
                var obj = cmd.ExecuteScalar();
                result = (long)obj;
            }
            connection.Close();
            return result;
        }

        public void ResetQueueSequence()
        {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = PostgressSql.ResetQueueSequenceVal();
                var obj = cmd.ExecuteScalar();
            }
            connection.Close();
        }

        public void SetQueueSequenceValue(long value)
        {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = PostgressSql.SetQueueSequenceVal(value);
                var obj = cmd.ExecuteScalar();
            }
            connection.Close();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("queuesequence");
            
            modelBuilder.Entity<Advertisement>()
                .Property(a => a.AdType)
                .HasConversion<int>();

            var categories = new[]
            {
                new {CategoryId = 1L, Title = "Games"},
                new {CategoryId = 2L, Title = "Computer science"},
                new {CategoryId = 3L, Title = "Guitar"},
                new {CategoryId = 4L, Title = "Social"}
            };

            var tags = new[]
            {
                new {TagId = 1L, Title = "Nice job"},
                new {TagId = 2L, Title = "Meet"},
                new {TagId = 3L, Title = "Life"},
                new {TagId = 4L, Title = "Films"},
                new {TagId = 5L, Title = "One more tag"}
            };

            var adds = new []
            {
                new
                {
                    AdvertisementId = 1L,
                    AdType = AdType.TextAd,
                    CategoryId = 1L,
                    Cost = 34.3m,
                    Content = "SomeTest",
                    Views = 34L
                },
                new
                {
                    AdvertisementId = 2L,
                    AdType = AdType.BannerAd,
                    CategoryId = 4L,
                    Cost = 15.0m,
                    Content = "Girls like dogs... Do you want to buy a dog?",
                    Views = 45233L
                },
                new
                {
                    AdvertisementId = 4L,
                    AdType = AdType.BannerAd,
                    CategoryId = 4L,
                    Cost = 15.0m,
                    Content = "<html><h1>Hello world</h1></html>",
                    Views = 2L
                }
            };

            modelBuilder.Entity<Category>()
                .HasData(categories);

            modelBuilder.Entity<Tag>()
                .HasData(tags);

            modelBuilder.Entity<Advertisement>()
                .HasData(adds);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}