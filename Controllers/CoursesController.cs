using Microsoft.AspNetCore.Mvc;
using UniversityCoursesAPI.Attributes;
using UniversityCoursesAPI.Models;
using UniversityCoursesAPI.Services;

namespace UniversityCoursesAPI.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly CourseService _courses;

    public CoursesController(CourseService courses)
    {
        _courses = courses;
    }

    [HttpGet]
    [BasicAuthentication]
    [MyAuthorize("Admin", "Teacher", "Student")]
    public ActionResult<List<Course>> GetAll()
    {
        return Ok(_courses.GetAll());
    }

    [HttpGet("{id:int}")]
    [BasicAuthentication]
    [MyAuthorize("Admin", "Teacher", "Student")]
    public ActionResult<Course> GetById(int id)
    {
        var course = _courses.Get(id);
        if (course == null)
            return NotFound(new { message = $"Курс с id={id} не найден" });

        return Ok(course);
    }

    [HttpPost]
    [BasicAuthentication]
    [MyAuthorize("Admin", "Teacher")]
    public IActionResult Create(Course course)
    {
        var created = _courses.Add(course);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [BasicAuthentication]
    [MyAuthorize("Admin", "Teacher")]
    public IActionResult Update(int id, Course course)
    {
        var updated = _courses.Update(id, course);
        if (updated == null)
            return NotFound(new { message = $"Курс с id={id} не найден" });

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [BasicAuthentication]
    [MyAuthorize("Admin")]
    public IActionResult Delete(int id)
    {
        if (!_courses.Delete(id))
            return NotFound(new { message = $"Курс с id={id} не найден" });

        return NoContent();
    }

    [HttpGet("info")]
    public IActionResult GetInfo()
    {
        return Ok(new
        {
            university = "Университет",
            description = "Система управления учебными курсами",
            coursesCount = _courses.GetAll().Count
        });
    }
}
