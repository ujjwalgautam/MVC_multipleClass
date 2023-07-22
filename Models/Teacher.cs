using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_multipleClass.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Subject")]
        
        public int SId { get; set; }
        public Subject Subject { get; set; }
        public string Address { get; set; }
    }
}
