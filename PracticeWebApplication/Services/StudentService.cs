using Microsoft.Identity.Client;
using PracticeWebApplication.Data;
using PracticeWebApplication.Dtos;
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

    public object StudentDetails { get; internal set; }

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

    //public StudentDetailsDto? GetStudentById(int id)
    //{
    //    StudentDetails? student = _context.StudentDetails.Find(id);
    //    if (student == null)
    //    {
    //        return null;
    //    }
    //    student= _context.StudentDetails.FirstOrDefault();
    //}


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

    public bool CreateStudent(CreateStudentRequest request)
    {
        try
        {
            var student = _context.StudentDetails.FirstOrDefault(s => s.StudentName == request.StudentName);
            if (student is not null) throw new Exception("Student with name {StudentName} already exists.");
            student = new StudentDetails
            {
                StudentName = request.StudentName,
                FatherName = request.FatherName,
                MotherName = request.MotherName,
                Gender = request.Gender,
                Address = request.Address
            };
            _context.StudentDetails.Add(student);
            _context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "An error occurred while creating a student with {StudentName}.", request.StudentName);
            //return false;
            _logger.LogError(ex, "An error occurred while creating a student with {@newStudent}", request);
            return false;
        }
    }

    public StudentDetailsDto? UpdateStudent(int id, CreateStudentRequest request)
    {
        try
        {
            StudentDetails? student = _context.StudentDetails.Find(id);
            if (student == null)
            {
                return null;
            }
            student.StudentName = request.StudentName;
            student.FatherName = request.FatherName;
            student.MotherName = request.MotherName;
            student.Gender = request.Gender;
            student.Address = request.Address;

            _context.SaveChanges();

            return new StudentDetailsDto(student.ID, student.StudentName, student.FatherName, student.MotherName, student.Gender, student.Address);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a student with {@updateStudent}", request);
            return null;
        }
    }

    public StudentDetailsDto? GetStudentById(int id)
    {
        try
        {
            StudentDetails? student = _context.StudentDetails.Find(id);
            if (student == null)
            {
                return null;
            }
            return new StudentDetailsDto(student.ID, student.StudentName, student.FatherName, student.MotherName, student.Gender,student.Address);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving a student with ID {StudentId}.", id);
            return null;
        }
    }
 }
