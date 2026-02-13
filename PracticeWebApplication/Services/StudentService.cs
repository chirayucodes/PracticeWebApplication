using Microsoft.EntityFrameworkCore;
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
                Address = s.Address,
                IsActive = s.IsActive
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
            Address = model.Address,
            IsActive = model.IsActive
        };

        _context.StudentDetails.Add(student);
        _context.SaveChanges();

        return true;
    }

    public StudentDetailsDto? CreateStudent(CreateStudentRequest request)
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
                Address = request.Address,
                IsActive = request.IsActive
            };
            _context.StudentDetails.Add(student);
            _context.SaveChanges();

            return new StudentDetailsDto(
                student.ID,
                student.StudentName,
                student.FatherName,
                student.MotherName,
                student.Gender,
                student.Address,
                student.IsActive
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a student with {@newStudent}", request);
            return null;
        }
    }

    public StudentDetailsDto? UpdateStudent(int id, CreateStudentRequest request)
    {
        try
        {
            var student = _context.StudentDetails.Find(id);
            if (student is null) return null;

            var studentNameExists = _context.StudentDetails
                .Any(s => s.StudentName == request.StudentName && s.ID != id);

            if (studentNameExists) return null;

            student.StudentName = request.StudentName;
            student.FatherName = request.FatherName;
            student.MotherName = request.MotherName;
            student.Gender = request.Gender;
            student.Address = request.Address;
            student.IsActive = request.IsActive;

            _context.SaveChanges();

            return new StudentDetailsDto(student.ID, student.StudentName, student.FatherName, student.MotherName,
                student.Gender, student.Address, student.IsActive);
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
            var student = _context.StudentDetails.Find(id);
            if (student == null) return null;
            return new StudentDetailsDto(student.ID, student.StudentName, student.FatherName, student.MotherName,
                student.Gender, student.Address, student.IsActive);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving a student with ID {StudentId}.", id);
            return null;
        }
    }

    public StudentDetailsDto? DeleteStudent(int id)
        {
            try
            {
                var student = _context.StudentDetails.FirstOrDefault(s => s.ID == id);

                if (student is null) return null;

                throw new ConflictException("Student with ID {StudentId} not found.");

            _context.StudentDetails.Remove(student);
            _context.SaveChanges();

                return new StudentDetailsDto(
                    student.ID, 
                    student.StudentName,
                    student.FatherName,
                    student.MotherName,
                    student.Gender,
                    student.Address,
                    student.IsActive);
        }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a student with ID {StudentId}.", id);
                return false;
            }
        catch (ConflictException ex)
        {
            _logger.LogError(ex, "Error while creating a state with StudentID {StudentID}. Some conflicts occured.",
                StudentID);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,
                "Database error while deleting student with ID {StudentID}", id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while deleting student with ID {StudentID}", id);
        }

        return null;

    }

}