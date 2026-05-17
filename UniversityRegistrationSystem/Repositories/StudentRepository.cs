using Microsoft.EntityFrameworkCore;
using UniversityRegistrationSystem.Data;
using UniversityRegistrationSystem.Interfaces;
using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Student>> GetStudentsWithCoursesAsync()
    {
        return await _entities
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .ToListAsync();
    }
}
