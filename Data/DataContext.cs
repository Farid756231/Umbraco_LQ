using Microsoft.EntityFrameworkCore;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<QuizViewModel> Quizzes { get; set; }
        public DbSet<Prov1Result> Prov1results { get; set; }

        public DbSet<Prov2Quizz> Prov2Quizzs { get; set; }
        public DbSet<Prov2QuizzResult> Prov2QuizzResults { get; set; }

        public DbSet<Prov3Quizz> prov3Quizzs { get; set; }
        public DbSet<Prov3QuizzResult> prov3QuizzResults { get; set; }

        public DbSet<Reviwe> Reviews { get; set; }
    }
}
