using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegistrationSystem.DTOs;

namespace UniversityRegistrationSystem.Interfaces;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentResponseDto>> GetAllEnrollmentsAsync();
    Task<EnrollmentResponseDto?> GetEnrollmentByIdAsync(int id);
    Task<EnrollmentResponseDto> AddEnrollmentAsync(CreateEnrollmentDto dto);
    Task<EnrollmentResponseDto?> UpdateEnrollmentAsync(int id, UpdateEnrollmentDto dto);
    Task<bool> DeleteEnrollmentAsync(int id);
}
