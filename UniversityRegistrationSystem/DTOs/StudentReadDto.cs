namespace UniversityRegistrationSystem.DTOs;

public class StudentReadDto
{
    public int StudentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int MajorId { get; set; }
    public int CourseCount { get; set; }
}
