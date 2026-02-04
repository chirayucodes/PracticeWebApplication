using Microsoft.AspNetCore.Mvc;
using PracticeWebApplication.Data;

namespace PracticeWebApplication.Controllers
{
    public class StudentDetailControllers : Controller
    {
        public IActionResult Index()
        {
            AppDbContext dbContext = new();
            IReadOnlyList<StudentDetails> names = dbContext.StudentDetails
                           .Select(s => new StudentDetails
                            {
                            ID = s.ID,
                            StudentName = s.StudentName,
                            FatherName= s.FatherName,
                            MotherName = s.MotherName,
                            Gender= s.Gender,
                            Address = s.Address
                            }).ToList();

            return View(names);
        }
    }
}
