namespace PracticeWebApplication.Models;

public class StudentViewModel
{
    public int ID { get; set; }

    public required string StudentName { get; set; }

    public required string FatherName { get; set; }

    public required string MotherName { get; set; }

    public required string Gender { get; set; }

    public required string Address { get; set; }

    public required bool IsActive { get; set; } = true;


}