using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetStudentsWithCoursesAsync();
}
