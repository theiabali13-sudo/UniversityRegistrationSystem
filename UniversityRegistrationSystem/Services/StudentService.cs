using UniversityRegistrationSystem.Interfaces;
using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetStudentsWithCoursesAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();
        return student;
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null)
        {
            return false;
        }

        _studentRepository.Remove(student);
        return await _studentRepository.SaveChangesAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _studentRepository.SaveChangesAsync();
    }
}
