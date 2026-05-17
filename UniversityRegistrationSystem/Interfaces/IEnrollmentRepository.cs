using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Interfaces;

public interface IEnrollmentRepository : IGenericRepository<Enrollment>
{
    Task<IEnumerable<Enrollment>> GetEnrollmentsWithStudentAndCourseAsync();
    Task<Enrollment?> GetEnrollmentDetailsByIdAsync(int enrolId);
}
