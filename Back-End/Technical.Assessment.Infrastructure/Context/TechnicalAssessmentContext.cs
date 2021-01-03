using Microsoft.EntityFrameworkCore;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.Infrastructure.Context
{
    public class TechnicalAssessmentContext : DbContext
    {
        public TechnicalAssessmentContext()
        {

        }

        public TechnicalAssessmentContext(DbContextOptions<TechnicalAssessmentContext> options) : base(options)
        {

        }

        public DbSet<Respondent> Respondents { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOrder> QuestionOrders { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {

                modelBuilder.Entity<Response>().HasKey(table => new { table.QuestionId,table.RespondentId, table.SurverResponseId });
                modelBuilder.Entity<QuestionOrder>().HasKey(table => new { table.QuestionId,table.SurverId });

                modelBuilder.Entity<Respondent>().Property(e => e.DateCreated).HasDefaultValueSql("GETUTCDATE()");
                modelBuilder.Entity<Question>().Property(e => e.DateModified).HasDefaultValueSql("GETUTCDATE()");
                modelBuilder.Entity<Survey>().Property(e => e.DateModified).HasDefaultValueSql("GETUTCDATE()");
                modelBuilder.Entity<SurveyResponse>().Property(e => e.DateModified).HasDefaultValueSql("GETUTCDATE()");

                modelBuilder.Entity<Respondent>().Ignore(c => c.DateModified);
                modelBuilder.Entity<Question>().Ignore(c => c.DateCreated);
                modelBuilder.Entity<Survey>().Ignore(c => c.DateCreated);
                modelBuilder.Entity<SurveyResponse>().Ignore(c => c.DateCreated);

                modelBuilder.Entity<Question>().Ignore(c => c.Order);
                modelBuilder.Entity<Question>().Ignore(c => c.SurverId);
            }
        }

    }
}
