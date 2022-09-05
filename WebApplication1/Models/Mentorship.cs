using Calischool.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calischool.Models
{
    public class Mentorship 
    { 
        [Key]
        public int? Id { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CourseToMentor { get; set; }
        public string? NameOfTheMentor { get; set; }
        public string? PhoneNumberOfMentor { get; set; }
        public string? CourseDuration { get; set; }
        public bool IsAssigned { get; set; }

        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public bool ErrorHappened { get; set; }
    }
}
