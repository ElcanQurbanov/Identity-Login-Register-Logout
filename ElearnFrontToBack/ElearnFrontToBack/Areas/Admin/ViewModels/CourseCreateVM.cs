using ElearnFrontToBack.Models;
using System.ComponentModel.DataAnnotations;

namespace ElearnFrontToBack.Areas.Admin.ViewModels
{
    public class CourseCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Sales { get; set; }

        public int AuthorId { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
