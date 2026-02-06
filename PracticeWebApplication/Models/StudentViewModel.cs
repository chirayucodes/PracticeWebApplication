using System.ComponentModel.DataAnnotations;

namespace PracticeWebApplication.Models
{
    public class StudentViewModel
    {
        public int ID { get; set; }

        public string? StudentName { get; set; }

        public string? FatherName { get; set; }

        public string? MotherName { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }


    }
}
