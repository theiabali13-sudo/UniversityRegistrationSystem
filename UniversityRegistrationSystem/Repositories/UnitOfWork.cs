using System.Threading.Tasks;
using UniversityRegistrationSystem.Data;
using UniversityRegistrationSystem.Interfaces;

namespace UniversityRegistrationSystem.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private EnrollmentRepository? _enrollmentRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IEnrollmentRepository Enrollments => _enrollmentRepository ??= new EnrollmentRepository(_context);

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
