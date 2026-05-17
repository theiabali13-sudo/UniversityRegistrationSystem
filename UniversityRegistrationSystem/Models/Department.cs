using System.ComponentModel.DataAnnotations;

namespace UniversityRegistrationSystem.Models;

public class Department
{
    [Key]
    public int DeptId { get; set; }

    [Required]
    [MaxLength(100)]
    public string DeptName { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
