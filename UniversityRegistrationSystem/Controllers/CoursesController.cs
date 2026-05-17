using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityRegistrationSystem.Data;
using UniversityRegistrationSystem.DTOs;

namespace UniversityRegistrationSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CoursesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetAll()
    {
        var courses = await _context.Courses
            .AsNoTracking()
            .Select(c => new CourseReadDto
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Credits = c.Credits,
                DeptId = c.DeptId
            })
            .ToListAsync();

        return Ok(courses);
    }
}
