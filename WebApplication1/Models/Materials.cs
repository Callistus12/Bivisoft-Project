using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calischool.Models
{
    [Table("student materials")]
    public class Materials
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string CourseTitle { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string FileUrl { get; set; }

        [NotMapped]
        public IFormFile FileUploadedFromDevice { get; set; }

        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public bool ErrorHappened { get; set; }
        public bool CloseFile { get; set; }
    }
}
