namespace UniversityRegistrationSystem.DTOs;

public class CourseReadDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Credits { get; set; }
    public int DeptId { get; set; }
}
