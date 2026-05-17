using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityRegistrationSystem.Models;

public class Enrollment
{
    [Key]
    public int EnrolId { get; set; }

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }

    public Student? Student { get; set; }

    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }

    public Course? Course { get; set; }

    [Required]
    [MaxLength(20)]
    public string Semester { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }

    public Grade? Grade { get; set; }
}
