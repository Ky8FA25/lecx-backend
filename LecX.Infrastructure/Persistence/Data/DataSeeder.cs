using Google.Protobuf.Reflection;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LecX.Infrastructure.Persistence.Data
{
    public class DataSeeder
    {
        private readonly DbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DataSeeder(DbContext db, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAllAsync()
        {
            //await _db.Database.MigrateAsync();
            //await SeedRolesAsync();

            //var users = await SeedUsersAsync();
            //await SeedCategoriesAsync();
            //await SeedInstructorsAsync(users.InstructorUser);  
            //await SeedCoursesAsync(users.InstructorUser);       
            await SeedLectuesAsync(3);                         
            await SeedComments(1);                             
        }

        private async Task SeedRolesAsync()
        {
            var roles = new[]
            {
                new IdentityRole { Id = "1", Name = "Admin",      NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Student",    NormalizedName = "STUDENT" },
                new IdentityRole { Id = "3", Name = "Instructor", NormalizedName = "INSTRUCTOR" },
            };

            foreach (var r in roles)
            {
                var exists = await _roleManager.FindByIdAsync(r.Id);
                if (exists == null)
                {
                    var result = await _roleManager.CreateAsync(r);
                    if (!result.Succeeded)
                        throw new Exception($"Create role {r.Name} failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private async Task<(User AdminUser, User StudentUser, User InstructorUser)> SeedUsersAsync()
        {
            async Task<User> EnsureUserAsync(string id, string userName, string email, string role)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (!await _userManager.IsInRoleAsync(user, role))
                        await _userManager.AddToRoleAsync(user, role);
                    return user;
                }

                var newUser = new User
                {
                    Id = id,
                    UserName = userName,
                    NormalizedUserName = userName.ToUpperInvariant(),
                    Email = email,
                    NormalizedEmail = email.ToUpperInvariant(),
                    EmailConfirmed = true,

                    FirstName = userName,
                    LastName = "Default",
                    Address = "VN",
                    Dob = new DateTime(2000, 1, 1),
                    Gender = Gender.Male,                 // nếu User.Gender là enum
                    ProfileImagePath = "/assets/images/default.jpg",
                    WalletUser = 0.0                      // nên là decimal trong entity
                };

                var result = await _userManager.CreateAsync(newUser, "Abcd1234!");
                if (!result.Succeeded)
                    throw new Exception($"Create user {userName} failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                await _userManager.AddToRoleAsync(newUser, role);
                return newUser;
            }

            var admin = await EnsureUserAsync("admin-0001", "admin", "admin@demo.com", "Admin");
            var student = await EnsureUserAsync("student-0001", "student", "student@demo.com", "Student");
            var instructor = await EnsureUserAsync("instructor-0001", "instructor", "instructor@demo.com", "Instructor");

            return (admin, student, instructor);
        }

        private async Task SeedCategoriesAsync()
        {
            var catSet = _db.Set<Category>();
            if (await catSet.AnyAsync()) return;

            // FullName là string
            var categories = new[]
            {
                new Category { FullName = "Programming",    Description = "Courses related to programming and software development." },
                new Category { FullName = "Data Science",    Description = "Courses focused on data analysis and machine learning." },
                new Category { FullName = "Web Development", Description = "Courses for building websites and web applications." },
                new Category { FullName = "Design",          Description = "Courses for graphic design and multimedia." },
            };

            await catSet.AddRangeAsync(categories);
            await _db.SaveChangesAsync();
        }

        private async Task SeedInstructorsAsync(User instructorUser)
        {
            var insSet = _db.Set<Instructor>();
            // shared primary key: InstructorId == User.Id
            var exists = await insSet.AnyAsync(i => i.InstructorId == instructorUser.Id);
            if (exists) return;

            await insSet.AddAsync(new Instructor
            {
                InstructorId = instructorUser.Id,
                Bio = "Default instructor profile"
            });
            await _db.SaveChangesAsync();
        }

        private async Task SeedCoursesAsync(User instructorUser)
        {
            var courseSet = _db.Set<Course>();
            if (await courseSet.AnyAsync()) return;

            var categories = await _db.Set<Category>().ToListAsync();
            if (!categories.Any()) return;

            int CatId(string fullName)
            {
                var found = categories.FirstOrDefault(c =>
                    string.Equals(c.FullName, fullName, StringComparison.OrdinalIgnoreCase));
                return found?.CategoryId ?? categories.First().CategoryId;
            }

            var programmingId = CatId("Programming");
            var dataScienceId = CatId("Data Science");
            var webDevId = CatId("Web Development");
            var designId = CatId("Design");

            var courses = new List<Course>
            {
                // CategoryID = 1 (Programming)
                new Course {
                    Title = "Python for Beginners",
                    CourseCode = "CS101",
                    Description = "Learn Python programming from scratch.",
                    CoverImagePath = "/Images/cover/python.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 100.00m,
                    CategoryId = programmingId,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-01"),
                    LastUpdate = DateTime.Parse("2024-09-20"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Java Advanced Techniques",
                    CourseCode = "CS102",
                    Description = "Explore advanced Java programming concepts.",
                    CoverImagePath = "/Images/cover/java.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 120.00m,
                    CategoryId = programmingId,
                    Level = CourseLevel.Advanced,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-05"),
                    LastUpdate = DateTime.Parse("2024-09-21"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "C# for Beginners",
                    CourseCode = "CS103",
                    Description = "Introduction to C# programming.",
                    CoverImagePath = "/Images/cover/csharp.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 90.00m,
                    CategoryId = programmingId,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-10"),
                    LastUpdate = DateTime.Parse("2024-09-22"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Full-Stack Development with Node.js",
                    CourseCode = "CS104",
                    Description = "Learn full-stack development using Node.js.",
                    CoverImagePath = "/Images/cover/nodejs.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 150.00m,
                    CategoryId = programmingId,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-15"),
                    LastUpdate = DateTime.Parse("2024-09-23"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Introduction to Algorithms",
                    CourseCode = "CS105",
                    Description = "Learn about algorithms and data structures.",
                    CoverImagePath = "/Images/cover/algorithms.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 110.00m,
                    CategoryId = programmingId,
                    Level = CourseLevel.Advanced,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-20"),
                    LastUpdate = DateTime.Parse("2024-09-24"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },

                // CategoryID = 2 (Data Science)
                new Course {
                    Title = "Data Analysis with R",
                    CourseCode = "DS101",
                    Description = "Learn data analysis using R programming.",
                    CoverImagePath = "/Images/cover/data_analysis.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 130.00m,
                    CategoryId = dataScienceId,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-01"),
                    LastUpdate = DateTime.Parse("2024-09-20"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Machine Learning A-Z",
                    CourseCode = "DS102",
                    Description = "Master machine learning algorithms and techniques.",
                    CoverImagePath = "/Images/cover/machine_learning.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 200.00m,
                    CategoryId = dataScienceId,
                    Level = CourseLevel.Advanced,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-05"),
                    LastUpdate = DateTime.Parse("2024-09-21"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Data Science for Everyone",
                    CourseCode = "DS103",
                    Description = "Introduction to data science concepts.",
                    CoverImagePath = "/Images/cover/data_science.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 100.00m,
                    CategoryId = dataScienceId,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-10"),
                    LastUpdate = DateTime.Parse("2024-09-22"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Deep Learning with TensorFlow",
                    CourseCode = "DS104",
                    Description = "Explore deep learning with TensorFlow.",
                    CoverImagePath = "/Images/cover/deep_learning.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 150.00m,
                    CategoryId = dataScienceId,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-15"),
                    LastUpdate = DateTime.Parse("2024-09-23"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Statistics for Data Science",
                    CourseCode = "DS105",
                    Description = "Learn statistics for data science applications.",
                    CoverImagePath = "/Images/cover/statistics.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 120.00m,
                    CategoryId = dataScienceId,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-20"),
                    LastUpdate = DateTime.Parse("2024-09-24"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },

                // CategoryID = 3 (Web Development)
                new Course {
                    Title = "HTML & CSS for Beginners",
                    CourseCode = "WD101",
                    Description = "Learn the basics of web development with HTML & CSS.",
                    CoverImagePath = "/Images/cover/html_css.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 80.00m,
                    CategoryId = webDevId,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-01"),
                    LastUpdate = DateTime.Parse("2024-09-20"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "JavaScript Essentials",
                    CourseCode = "WD102",
                    Description = "Get started with JavaScript for web development.",
                    CoverImagePath = "/Images/cover/javascript.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 100.00m,
                    CategoryId = webDevId,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-05"),
                    LastUpdate = DateTime.Parse("2024-09-21"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "React for Beginners",
                    CourseCode = "WD103",
                    Description = "Build user interfaces with React.",
                    CoverImagePath = "/Images/cover/react.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 150.00m,
                    CategoryId = webDevId,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-10"),
                    LastUpdate = DateTime.Parse("2024-09-22"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Node.js for Beginners",
                    CourseCode = "WD104",
                    Description = "Learn how to build web applications using Node.js.",
                    CoverImagePath = "/Images/cover/nodejs_web.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 120.00m,
                    CategoryId = webDevId,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-15"),
                    LastUpdate = DateTime.Parse("2024-09-23"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
                new Course {
                    Title = "Advanced CSS Techniques",
                    CourseCode = "WD105",
                    Description = "Explore advanced techniques in CSS for modern web design.",
                    CoverImagePath = "/Images/cover/advanced_css.jpg",
                    InstructorId = instructorUser.Id,
                    NumberOfStudents = 0,
                    Price = 130.00m,
                    CategoryId = webDevId,
                    Level = CourseLevel.Advanced,
                    Status = CourseStatus.Archived,
                    IsBaned = false,
                    CreateDate = DateTime.Parse("2024-09-20"),
                    LastUpdate = DateTime.Parse("2024-09-24"),
                    EndDate = DateTime.Parse("2024-12-20"),
                    NumberOfRate = 0,
                    Rating = 0
                },
            };


            await courseSet.AddRangeAsync(courses);
            await _db.SaveChangesAsync();
        }

        private async Task SeedLectuesAsync(int courseId)
        {
            var lectureSet = _db.Set<Lecture>();
            if (await lectureSet.AnyAsync()) return;
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    LectureId = 1,
                    CourseId = 1,
                    Title = "Introduction to C# Programming",
                    Description = "Overview of C# basics and .NET ecosystem.",
                    UpLoadDate = DateTime.Now.AddDays(-10)
                },
                new Lecture
                {
                    LectureId = 2,
                    CourseId = 1,
                    Title = "Object-Oriented Programming in C#",
                    Description = "Understanding classes, inheritance, and polymorphism.",
                    UpLoadDate = DateTime.Now.AddDays(-8)
                },
                new Lecture
                {
                    LectureId = 3,
                    CourseId = 2,
                    Title = "Database Fundamentals",
                    Description = "Introduction to SQL and relational database design.",
                    UpLoadDate = DateTime.Now.AddDays(-5)
                },
                new Lecture
                {
                    LectureId = 4,
                    CourseId = 2,
                    Title = "Entity Framework Core Basics",
                    Description = "Mapping entities and performing CRUD with EF Core.",
                    UpLoadDate = DateTime.Now.AddDays(-3)
                }
            };
            await lectureSet.AddRangeAsync(lectures);
            await _db.SaveChangesAsync();
        }

        private async Task SeedComments(int lectureId)
        {
            var commentSet = _db.Set<Comment>();
            if (await commentSet.AnyAsync()) return;
            var comments = new List<Comment>
            {
                new Comment
                {
                    CommentId = 1,
                    LectureId = lectureId,
                    UserId = "student-0001",
                    Content = "This introduction was really clear and helpful!",
                    Timestamp = DateTime.Now.AddDays(-9)
                },
                new Comment
                {
                    CommentId = 2,
                    LectureId = lectureId,
                    UserId = "student-0001",
                    Content = "Can you explain more about CLR?",
                    Timestamp = DateTime.Now.AddDays(-8)
                },
                new Comment
                {
                    CommentId = 3,
                    LectureId = lectureId,
                    UserId = "student-0001",
                    Content = "The OOP examples were great!",
                    Timestamp = DateTime.Now.AddDays(-7)
                },
                new Comment
                {
                    CommentId = 4,
                    LectureId = lectureId,
                    UserId = "student-0001",
                    Content = "Agree! Especially the inheritance demo 👍",
                    ParentCmtId = 3, // reply
                    Timestamp = DateTime.Now.AddDays(-7).AddHours(2)
                },
                new Comment
                {
                    CommentId = 5,
                    LectureId = lectureId,
                    UserId = "student-0001",
                    Content = "Could you provide the SQL script from the lecture?",
                    Timestamp = DateTime.Now.AddDays(-4)
                },
                new Comment
                {
                    CommentId = 6,
                    LectureId = lectureId,
                    UserId = "student-0001",
                    Content = "EF Core makes things so much easier!",
                    Timestamp = DateTime.Now.AddDays(-2)
                }
            };
            await commentSet.AddRangeAsync(comments);
            await _db.SaveChangesAsync();
        }
    }
}
