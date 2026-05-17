using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityRegistrationSystem.Models;

public class Instructor
{
    [Key]
    public int InstId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(Department))]
    public int DeptId { get; set; }

    public Department? Department { get; set; }
}
