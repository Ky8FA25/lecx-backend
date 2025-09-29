using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Abstractions
{
    public interface IAppDbContext
    {
        public DbSet<Category> Categories { get; }
        public DbSet<Instructor> Instructors { get; }
        public DbSet<Course> Courses { get; }
        public DbSet<Lecture> Lectures { get; }
        public DbSet<LectureFile> LectureFiles { get; }
        public DbSet<LectureCompletion> LectureCompletions { get; }
        public DbSet<Comment> Comments { get; }
        public DbSet<CommentFile> CommentFiles { get; }
        public DbSet<BookMark> BookMarks { get; }
        public DbSet<Certificate> Certificates { get; }
        public DbSet<CourseMaterial> CourseMaterials { get; }
        public DbSet<Assignment> Assignments { get; }
        public DbSet<Submission> Submissions { get; }
        public DbSet<AssignmentScore> AssignmentScores { get; }
        public DbSet<Test> Tests { get; }
        public DbSet<Question> Questions { get; }
        public DbSet<TestScore> TestScore { get; }
        public DbSet<StudentCourse> StudentCourses { get; }
        public DbSet<Review> Reviews { get; }
        public DbSet<Payment> Payments { get; }
        public DbSet<Notification> Notifications { get; }
        public DbSet<Report> Reports { get; }
        public DbSet<Refund> RequestTranfers { get; }
        public DbSet<InstructorConfirmation> InstructorConfirmations { get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
