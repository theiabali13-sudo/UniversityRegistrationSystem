using System.Threading.Tasks;

namespace UniversityRegistrationSystem.Interfaces;

public interface IUnitOfWork
{
    IEnrollmentRepository Enrollments { get; }
    Task<int> SaveAsync();
}
