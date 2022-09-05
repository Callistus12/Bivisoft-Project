using System.ComponentModel.DataAnnotations.Schema;

namespace Calischool.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public virtual IEnumerable<ApplicationUser> Users { get; set; }
        public virtual IEnumerable<Materials> StudentMaterials { get; set; }
        public virtual IEnumerable<Mentorship> StudentMentorship { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public bool ErrorHappened { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
       
    }
}