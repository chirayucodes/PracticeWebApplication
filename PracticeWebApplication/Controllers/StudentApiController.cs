using Microsoft.AspNetCore.Mvc;
using PracticeWebApplication.Data;
using PracticeWebApplication.Dtos;

namespace PracticeWebApplication.Controllers

{
    public sealed class StudentApiController : ControllerBase
    {
        private readonly AppDbContext? dbContext;

        public StudentApiController(AppDbContext? dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }

        [HttpGet]
        [Route("/api/testdbb/students")]
        public IActionResult GetStudents()
        {
            IList<StudentDetailsDto> List = dbContext.StudentDetails
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
            StudentDetails? student = dbContext.StudentDetails.Find(id);
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
