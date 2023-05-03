using ElearnFrontToBack.Areas.Admin.ViewModels;
using ElearnFrontToBack.Data;
using ElearnFrontToBack.Helpers;
using ElearnFrontToBack.Models;
using ElearnFrontToBack.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ElearnFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICourseService _courseService;
        private readonly IAuthorService _authorService;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context,
                                IWebHostEnvironment env,
                                ICourseService courseService,
                                IAuthorService authorService)
        {
            _context = context;
            _courseService = courseService;
            _authorService = authorService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = await _context.Courses.Where(m => !m.SoftDelete).Include(m => m.CourseImages).Include(m => m.Author).ToListAsync();

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Course course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);

            IEnumerable<Course> courses = await _context.Courses.Where(m => !m.SoftDelete).Include(m => m.CourseImages).Include(m => m.Author).ToListAsync();

            if (course is null) return NotFound();

            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.courses = await GetCoursesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM model)
        {
            try
            {
                ViewBag.courses = await GetCoursesAsync();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                foreach (var photo in model.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }

                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View();
                    }
                }

                List<CourseImage> courseImages = new();

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    CourseImage courseImage = new()
                    {
                        Image = fileName,
                    };

                    courseImages.Add(courseImage);
                }

                courseImages.FirstOrDefault().IsMain = true;


                Course newCourse = new()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Sales = model.Sales,
                    AuthorId = model.AuthorId,
                    CourseImages = courseImages
                };

                await _context.CourseImages.AddRangeAsync(courseImages);
                await _context.Courses.AddAsync(newCourse);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Course course = await _courseService.GetFullDataById((int)id);

                //Course course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);

                if (course is null) return NotFound();

                foreach (var item in course.CourseImages)
                {
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "images", item.Image);

                    FileHelper.DeleteFile(path);
                }

                _context.Courses.Remove(course);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<SelectList> GetCoursesAsync()
        {
            IEnumerable<Author> courses = await _authorService.GetAll();
            return new SelectList(courses, "Id", "Name");
        }



    }
}
