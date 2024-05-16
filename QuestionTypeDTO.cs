

using System.ComponentModel.DataAnnotations;

namespace UnitPractical.DTO
{
    public class QuestionTypeDTO
    {
        [Key]
        public int QuestionID { get; set; }
        [Required]
        [StringLength(255)] // Adjust the maximum length as needed
        public string QuestionTypeName { get; set; }
    }
}
