

using Microsoft.EntityFrameworkCore;
using UnitPractical.Model;

namespace UnitPractical.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) 
            : base(options)
        {
        }
        
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Question> Questions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionType>()
                .ToContainer("QuestionType")
                .HasPartitionKey(c => c.QuestionID)
                .HasKey(q => q.QuestionID); 

            modelBuilder.Entity<Question>()
                .ToContainer("Question")
                .HasPartitionKey(e => e.Id);

            modelBuilder.Entity<UserInfo>()
                .ToContainer("UserInfo")
                .HasPartitionKey(e => e.ID);

        }


    }
}
