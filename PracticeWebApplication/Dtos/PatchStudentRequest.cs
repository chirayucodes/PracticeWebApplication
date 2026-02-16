using System.ComponentModel.DataAnnotations;

namespace PracticeWebApplication.Dtos
{
    public class PatchStudentRequest
    {
       [Required]  public required bool IsActive { get; init; }
    }
}
