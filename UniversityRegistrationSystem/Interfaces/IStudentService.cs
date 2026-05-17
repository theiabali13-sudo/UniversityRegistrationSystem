using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);
    Task<Student> CreateStudentAsync(Student student);
    Task<bool> DeleteStudentAsync(int id);
    Task<bool> SaveChangesAsync();
}
