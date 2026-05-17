using Microsoft.EntityFrameworkCore;
using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Grade> Grades => Set<Grade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Major)
            .WithMany(d => d.Students)
            .HasForeignKey(s => s.MajorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Instructor>()
            .HasOne(i => i.Department)
            .WithMany(d => d.Instructors)
            .HasForeignKey(i => i.DeptId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DeptId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Enrollment)
            .WithOne(e => e.Grade)
            .HasForeignKey<Grade>(g => g.EnrolId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Grade>()
            .HasIndex(g => g.EnrolId)
            .IsUnique();

        // Seed sample data
        modelBuilder.Entity<Department>().HasData(
            new Department { DeptId = 1, DeptName = "Computer Science" },
            new Department { DeptId = 2, DeptName = "Mathematics" },
            new Department { DeptId = 3, DeptName = "Physics" }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student { StudentId = 1, Name = "Alice Johnson", Email = "alice.johnson@example.com", MajorId = 1 },
            new Student { StudentId = 2, Name = "Bob Martinez", Email = "bob.martinez@example.com", MajorId = 2 },
            new Student { StudentId = 3, Name = "Carol Lee", Email = "carol.lee@example.com", MajorId = 1 },
            new Student { StudentId = 4, Name = "Dave Kim", Email = "dave.kim@example.com", MajorId = 3 }
        );

        modelBuilder.Entity<Instructor>().HasData(
            new Instructor { InstId = 1, Name = "Dr. Susan Smith", DeptId = 1 },
            new Instructor { InstId = 2, Name = "Dr. Linh Nguyen", DeptId = 2 },
            new Instructor { InstId = 3, Name = "Dr. Raj Patel", DeptId = 3 }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { CourseId = 1, Title = "Algorithms", Credits = 4, DeptId = 1 },
            new Course { CourseId = 2, Title = "Data Structures", Credits = 3, DeptId = 1 },
            new Course { CourseId = 3, Title = "Calculus I", Credits = 4, DeptId = 2 },
            new Course { CourseId = 4, Title = "Physics I", Credits = 4, DeptId = 3 }
        );

        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment { EnrolId = 1, StudentId = 1, CourseId = 1, Semester = "Fall", Year = 2023 },
            new Enrollment { EnrolId = 2, StudentId = 1, CourseId = 2, Semester = "Spring", Year = 2024 },
            new Enrollment { EnrolId = 3, StudentId = 2, CourseId = 3, Semester = "Fall", Year = 2023 },
            new Enrollment { EnrolId = 4, StudentId = 3, CourseId = 1, Semester = "Spring", Year = 2024 },
            new Enrollment { EnrolId = 5, StudentId = 4, CourseId = 4, Semester = "Fall", Year = 2023 }
        );

        modelBuilder.Entity<Grade>().HasData(
            new Grade { GradeId = 1, EnrolId = 1, NumericGrade = 93.5m, LetterGrade = "A" },
            new Grade { GradeId = 2, EnrolId = 2, NumericGrade = 88.0m, LetterGrade = "B+" },
            new Grade { GradeId = 3, EnrolId = 3, NumericGrade = 76.0m, LetterGrade = "C" },
            new Grade { GradeId = 4, EnrolId = 4, NumericGrade = 91.0m, LetterGrade = "A-" },
            new Grade { GradeId = 5, EnrolId = 5, NumericGrade = 82.5m, LetterGrade = "B-" }
        );

        base.OnModelCreating(modelBuilder);
    }
}
