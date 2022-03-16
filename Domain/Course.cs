using System.ComponentModel.DataAnnotations;

namespace CourseManager.Domain
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public int duration { get; set; }
        public string status { get; set; }
    }
}