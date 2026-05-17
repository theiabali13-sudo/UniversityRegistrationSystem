namespace UniversityRegistrationSystem.DTOs;

public class CreateEnrollmentDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string Semester { get; set; } = string.Empty;
    public int Year { get; set; }
}
