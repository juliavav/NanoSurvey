using Microsoft.EntityFrameworkCore;
using NanoSurvey.Data.Entities;

namespace NanoSurvey.Data
{
    public class SurveyContext : DbContext
    {
        private readonly DbContextOptions options;

        public SurveyContext(DbContextOptions options) : base(options)
        {
            this.options = options;
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Interview> Interviews { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>().HasIndex(i => i.Title);
            modelBuilder.Entity<Interview>().HasIndex(i => i.UserName);

            base.OnModelCreating(modelBuilder);
        }
    }
}