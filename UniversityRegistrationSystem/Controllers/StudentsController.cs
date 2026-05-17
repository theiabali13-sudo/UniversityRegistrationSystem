using Microsoft.AspNetCore.Mvc;
using UniversityRegistrationSystem.DTOs;
using UniversityRegistrationSystem.Interfaces;
using UniversityRegistrationSystem.Models;

namespace UniversityRegistrationSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetAll()
    {
        var students = await _studentService.GetAllStudentsAsync();

        var result = students.Select(s => new StudentReadDto
        {
            StudentId = s.StudentId,
            Name = s.Name,
            Email = s.Email,
            MajorId = s.MajorId,
            CourseCount = s.Enrollments.Count
        });

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentReadDto>> GetById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        var result = new StudentReadDto
        {
            StudentId = student.StudentId,
            Name = student.Name,
            Email = student.Email,
            MajorId = student.MajorId,
            CourseCount = student.Enrollments.Count
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<StudentReadDto>> Create(StudentCreateDto studentDto)
    {
        var newStudent = new Student
        {
            Name = studentDto.Name,
            Email = studentDto.Email,
            MajorId = studentDto.MajorId
        };

        var createdStudent = await _studentService.CreateStudentAsync(newStudent);

        var result = new StudentReadDto
        {
            StudentId = createdStudent.StudentId,
            Name = createdStudent.Name,
            Email = createdStudent.Email,
            MajorId = createdStudent.MajorId,
            CourseCount = createdStudent.Enrollments.Count
        };

        return CreatedAtAction(nameof(GetById), new { id = result.StudentId }, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _studentService.DeleteStudentAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
