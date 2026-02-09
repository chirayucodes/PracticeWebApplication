using PracticeWebApplication.Data;
using PracticeWebApplication.Models;


namespace PracticeWebApplication.Services;

public sealed class StudentService
{
    private readonly AppDbContext _context;
    private readonly ILogger<StudentService> _logger;

    public StudentService(AppDbContext context, ILogger<StudentService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
    }
    public IEnumerable<StudentViewModel> GetStudents()
    {
        IReadOnlyList<StudentViewModel> students = _context.StudentDetails
            .Select(s => new StudentViewModel
            {
                ID = s.ID,
                StudentName = s.StudentName,
                FatherName = s.FatherName,
                MotherName = s.MotherName,
                Gender = s.Gender,
                Address = s.Address
            }).ToArray();
        return students;
    }

    public bool CreateStudent(StudentViewModel model)
    {
        StudentDetails student = new()
        {
            StudentName = model.StudentName,
            FatherName = model.FatherName,
            MotherName = model.MotherName,
            Gender = model.Gender,
            Address = model.Address
        };

        _context.StudentDetails.Add(student);
        _context.SaveChanges();

        return true;
    }
}