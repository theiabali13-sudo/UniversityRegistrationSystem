namespace UniversityRegistrationSystem.DTOs;

public class EnrollmentResponseDto
{
    public int EnrolId { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public int Year { get; set; }
}
