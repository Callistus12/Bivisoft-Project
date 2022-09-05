using Calischool.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calischool.ViewModel
{
    public class RegisterViewModel 
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string RedsidentAddress { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string Qualification { get; set; }
        public string Displine { get; set; }        
        [MaxLength(6)]
        [MinLength(4)]
        public string Password { get; set; }
        [MaxLength(15)]
        [MinLength(11)]
        public string PhoneNumber { get; set; }
        public string ComfirmPassword { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public bool ErrorHappened { get; set; }
    }
}
