using System.Data.Entity;
using RydlewskiJablonski.Quiz.DAO.BO;

namespace RydlewskiJablonski.Quiz.DAO
{
    public class TestsContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<TestStatistic> TestStatistics { get; set; }
        public virtual DbSet<QuestionStatistic> QuestionStatistics { get; set; }
        public virtual DbSet<AnswerStatistic> AnswerStatistics { get; set; }
    }
}