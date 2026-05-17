namespace UniversityRegistrationSystem.Models;

public class Registration
{
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public string? Grade { get; set; }
}
