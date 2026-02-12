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
                s.Address
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
        StudentDetailsDto studentDto = new(
            student.ID,
            student.StudentName,
            student.FatherName,
            student.MotherName,
            student.Gender,
            student.Address
        );
        return Ok(studentDto);
    }

    [HttpPost]
    [Route("")]
    public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = _StudentService.CreateStudent(request);
        return Ok(result);
    }
}