using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegistrationSystem.Data;
using UniversityRegistrationSystem.Interfaces;
using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Repositories;

public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithStudentAndCourseAsync()
    {
        return await _entities
            .Include(e => e.Student)
            .Include(e => e.Course)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Enrollment?> GetEnrollmentDetailsByIdAsync(int enrolId)
    {
        return await _entities
            .Include(e => e.Student)
            .Include(e => e.Course)
            .Include(e => e.Grade)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EnrolId == enrolId);
    }
}
