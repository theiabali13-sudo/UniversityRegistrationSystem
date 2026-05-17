namespace UniversityRegistrationSystem.DTOs;

public class StudentCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int MajorId { get; set; }
}
