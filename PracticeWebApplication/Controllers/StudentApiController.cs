using Microsoft.AspNetCore.Mvc;
using PracticeWebApplication.Data;
using PracticeWebApplication.Dtos;

namespace PracticeWebApplication.Controllers

{
    public sealed class StudentApiController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public StudentApiController(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }

        [HttpGet]
        [Route("/api/testdbb/students")]
        public IActionResult GetStudents()
        {
            IList<StudentDetailsDto> List = _dbContext.StudentDetails
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
        [Route("/api/testdbb/students/{id}")]

        public IActionResult GetStudentById(int id)
        {
            StudentDetails? student = _dbContext.StudentDetails.Find(id);
            if (student == null)
            {
                return NotFound();
            }
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
    }
}
