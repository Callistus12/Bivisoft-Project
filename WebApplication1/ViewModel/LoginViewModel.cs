using Calischool.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calischool.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(6)]
        [MinLength(4)]
        public string Password { get; set; }
        public string RememberMe { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public bool ErrorHappened { get; set; }
    }
}
