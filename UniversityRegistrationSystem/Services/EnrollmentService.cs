using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegistrationSystem.DTOs;
using UniversityRegistrationSystem.Interfaces;
using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<EnrollmentResponseDto>> GetAllEnrollmentsAsync()
    {
        var enrollments = await _unitOfWork.Enrollments.GetEnrollmentsWithStudentAndCourseAsync();
        return enrollments.Select(e => new EnrollmentResponseDto
        {
            EnrolId = e.EnrolId,
            StudentId = e.StudentId,
            CourseId = e.CourseId,
            StudentName = e.Student?.Name ?? string.Empty,
            CourseTitle = e.Course?.Title ?? string.Empty,
            Semester = e.Semester,
            Year = e.Year
        });
    }

    public async Task<EnrollmentResponseDto?> GetEnrollmentByIdAsync(int id)
    {
        var e = await _unitOfWork.Enrollments.GetEnrollmentDetailsByIdAsync(id);
        if (e == null) return null;
        return new EnrollmentResponseDto
        {
            EnrolId = e.EnrolId,
            StudentId = e.StudentId,
            CourseId = e.CourseId,
            StudentName = e.Student?.Name ?? string.Empty,
            CourseTitle = e.Course?.Title ?? string.Empty,
            Semester = e.Semester,
            Year = e.Year
        };
    }

    public async Task<EnrollmentResponseDto> AddEnrollmentAsync(CreateEnrollmentDto dto)
    {
        var enrollment = new Enrollment
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId,
            Semester = dto.Semester,
            Year = dto.Year
        };

        await _unitOfWork.Enrollments.AddAsync(enrollment);
        await _unitOfWork.SaveAsync();

        var created = await _unitOfWork.Enrollments.GetEnrollmentDetailsByIdAsync(enrollment.EnrolId);
        return new EnrollmentResponseDto
        {
            EnrolId = created!.EnrolId,
            StudentName = created.Student?.Name ?? string.Empty,
            CourseTitle = created.Course?.Title ?? string.Empty,
            Semester = created.Semester,
            Year = created.Year
        };
    }

    public async Task<EnrollmentResponseDto?> UpdateEnrollmentAsync(int id, UpdateEnrollmentDto dto)
    {
        var existing = await _unitOfWork.Enrollments.GetByIdAsync(id);
        if (existing == null) return null;

        existing.StudentId = dto.StudentId;
        existing.CourseId = dto.CourseId;
        existing.Semester = dto.Semester;
        existing.Year = dto.Year;

        _unitOfWork.Enrollments.Update(existing);
        await _unitOfWork.SaveAsync();

        var updated = await _unitOfWork.Enrollments.GetEnrollmentDetailsByIdAsync(id);
        if (updated == null) return null;

        return new EnrollmentResponseDto
        {
            EnrolId = updated.EnrolId,
            StudentName = updated.Student?.Name ?? string.Empty,
            CourseTitle = updated.Course?.Title ?? string.Empty,
            Semester = updated.Semester,
            Year = updated.Year
        };
    }

    public async Task<bool> DeleteEnrollmentAsync(int id)
    {
        var existing = await _unitOfWork.Enrollments.GetByIdAsync(id);
        if (existing == null) return false;

        _unitOfWork.Enrollments.Delete(existing);
        var result = await _unitOfWork.SaveAsync();
        return result > 0;
    }
}
