using LecX.Application.Abstractions;
using LecX.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LecX.Infrastructure.Persistence.Data
{
    public class AppDbContext : IdentityDbContext<User>, IAppDbContext
    {
        // DbSets
        public DbSet<Category> Categories { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureFile> LectureFiles { get; set; }
        public DbSet<LectureCompletion> LectureCompletions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentFile> CommentFiles { get; set; }
        public DbSet<BookMark> BookMarks { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<AssignmentScore> AssignmentScores { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TestScore> TestScore { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Refund> RequestTranfers { get; set; }
        public DbSet<InstructorConfirmation> InstructorConfirmations { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder cb)
        {
            // string = varchar(255)
            cb.Properties<string>().HaveMaxLength(255).HaveColumnType("varchar(255)");
            // decimal
            cb.Properties<decimal>().HavePrecision(10, 3);
            // DateTime
            cb.Properties<DateTime>().HaveColumnType("datetime(6)");
            // enum
            cb.Properties<Enum>().HaveConversion<string>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            RemoveAspNetPrefixInIdentityTable(builder: builder);
        }

        private static void RemoveAspNetPrefixInIdentityTable(ModelBuilder builder)
        {
            const string AspNetPrefix = "AspNet";

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();

                if (tableName.StartsWith(value: AspNetPrefix))
                {
                    entityType.SetTableName(name: tableName[6..]);
                }
            }
        }
    }
}
