using ElearnFrontToBack.Models;

namespace ElearnFrontToBack.Areas.Admin.ViewModels
{
    public class CourseDetailVM
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public int Sales { get; set; }
        public int AuthorId { get; set; }
        public ICollection<CourseImage> CourseImages { get; set; }
    }
}
