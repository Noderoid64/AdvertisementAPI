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
            

            modelBuilder.Entity<Category>()
                .HasData(DefaultDbData.Categories);

            modelBuilder.Entity<Tag>()
                .HasData(DefaultDbData.Tags);

            modelBuilder.Entity<Advertisement>()
                .HasData(DefaultDbData.Ads);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}