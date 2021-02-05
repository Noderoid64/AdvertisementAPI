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

        public long GetNextQueueSequenceValue()
        {
            var connection = Database.GetDbConnection();
            connection.Open();
            long result = 0;
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = PostgressSQL.GetQueueSequenceVal();
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
                cmd.CommandText = PostgressSQL.ResetQueueSequenceVal();
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
                cmd.CommandText = PostgressSQL.SetQueueSequenceVal(value);
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


            base.OnModelCreating(modelBuilder);
        }
    }
}