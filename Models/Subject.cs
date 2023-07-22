using System.ComponentModel.DataAnnotations;

namespace MVC_multipleClass.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string SubjectName { get; set; }
    }
}
