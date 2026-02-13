using Microsoft.AspNetCore.Mvc;
using PracticeWebApplication.Dtos;
using PracticeWebApplication.Services;

namespace PracticeWebApplication.Controllers;

[Route("api/master/students")]
public sealed class StudentApiController : ControllerBase
{
    //private readonly AppDbContext _dbContext;
    private readonly StudentService _StudentService;

    public StudentApiController(StudentService StudentService)
    {
        _StudentService = StudentService ?? throw new ArgumentNullException(nameof(StudentService));
    }

    [HttpGet]
    [Route("")]
    public IActionResult GetStudents()
    {
        IList<StudentDetailsDto> List = _StudentService.GetStudents()
            .Select(s => new StudentDetailsDto(
                s.ID,
                s.StudentName,
                s.FatherName,
                s.MotherName,
                s.Gender,
                s.Address,
                s.IsActive
            )).ToList();
        return Ok(List);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _StudentService
            .GetStudents()
            .FirstOrDefault(s => s.ID == id);
        if (student == null) return NotFound();
        StudentDetailsDto studentDto = new
        (
            student.ID,
            student.StudentName,
            student.FatherName,
            student.MotherName,
            student.Gender,
            student.Address,
            student.IsActive
        );
        return Ok(studentDto);
    }

    [HttpPost]
    [Route("")]
    public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var studentDetailsDto = _StudentService.CreateStudent(request);
        return studentDetailsDto is null
            ? Problem("There was some problem, Check log for more details ")
            : Ok(studentDetailsDto);
    }


    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateStudent([FromBody] CreateStudentRequest request, int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = _StudentService.UpdateStudent(id, request);
        return result is null ? NotFound() : Ok(result);
    }
}