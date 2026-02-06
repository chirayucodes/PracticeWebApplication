using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeWebApplication.Data;
using PracticeWebApplication.Models;

namespace PracticeWebApplication.Controllers
{
    public class StudentDetailControllers : Controller
    {
        //private readonly AppDbContext _context;

        //public StudentDetailControllers(AppDbContext context)
        //{
        //    _context = context;
        //}

        public IActionResult Index()
        {
            AppDbContext dbContext = new();
            IEnumerable<StudentViewModel> names = dbContext.StudentDetails
                           .Select(s => new StudentViewModel
                           {
                               ID = s.ID,
                               StudentName = s.StudentName,
                               FatherName = s.FatherName,
                               MotherName = s.MotherName,
                               Gender = s.Gender,
                               Address = s.Address
                           }).ToList();
            return View(names);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentViewModel model)
        {
        
            if (!ModelState.IsValid)
            {
                return View( model);
            }
            AppDbContext dbContext = new();
            StudentDetails Student= new()
            {
                StudentName = model.StudentName,
                FatherName = model.FatherName,
                MotherName = model.MotherName,
                Gender = model.Gender,
                Address = model.Address
            };
            dbContext.StudentDetails
                .Add(Student);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
