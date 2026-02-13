namespace PracticeWebApplication.Dtos;

public sealed class CreateStudentRequest
{
    public required string StudentName { get; init; }
    public required string FatherName { get; init; }
    public required string MotherName { get; init; }
    public required string Gender { get; init; }
    public required string Address { get; init; }
    public required bool IsActive { get; init; }
}