
using System.ComponentModel.DataAnnotations;

namespace Mooshak_2._0.Controllers
{
    public class CourseViewModel
    {
        public int ID { get; set; }
       
        [Required]
        public string Name { get; set; }
    }
}