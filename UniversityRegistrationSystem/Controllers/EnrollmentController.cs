using Microsoft.AspNetCore.Mvc;
using UniversityRegistrationSystem.DTOs;
using UniversityRegistrationSystem.Interfaces;

namespace UniversityRegistrationSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnrollmentResponseDto>>> GetAll()
    {
        var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
        return Ok(enrollments);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EnrollmentResponseDto>> GetById(int id)
    {
        var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
        if (enrollment is null)
        {
            return NotFound();
        }

        return Ok(enrollment);
    }

    [HttpPost]
    public async Task<ActionResult<EnrollmentResponseDto>> Create(CreateEnrollmentDto dto)
    {
        var createdEnrollment = await _enrollmentService.AddEnrollmentAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdEnrollment.EnrolId }, createdEnrollment);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateEnrollmentDto dto)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var updatedEnrollment = await _enrollmentService.UpdateEnrollmentAsync(id, dto);
        if (updatedEnrollment is null)
        {
            return NotFound();
        }

        return Ok(updatedEnrollment);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _enrollmentService.DeleteEnrollmentAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
