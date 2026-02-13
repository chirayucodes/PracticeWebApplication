namespace PracticeWebApplication.Dtos;

public sealed class StudentDetailsDto(
    int ID,
    string StudentName,
    string FatherName,
    string MotherName,
    string Gender,
    string Address,
    bool IsActive)
{
    public int ID { get; } = ID;
    public string StudentName { get; } = StudentName;
    public string FatherName { get; } = FatherName;
    public string MotherName { get; } = MotherName;
    public string Gender { get; } = Gender;
    public string Address { get; } = Address;
    public bool IsActive { get; } = IsActive;
}