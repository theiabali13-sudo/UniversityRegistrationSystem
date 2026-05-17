using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityRegistrationSystem.Models;

public class Grade
{
    [Key]
    public int GradeId { get; set; }

    [ForeignKey(nameof(Enrollment))]
    public int EnrolId { get; set; }

    public Enrollment? Enrollment { get; set; }

    [Required]
    public decimal NumericGrade { get; set; }

    [Required]
    [MaxLength(5)]
    public string LetterGrade { get; set; } = string.Empty;
}
