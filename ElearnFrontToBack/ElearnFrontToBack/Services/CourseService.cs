using ElearnFrontToBack.Data;
using ElearnFrontToBack.Models;
using ElearnFrontToBack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElearnFrontToBack.Services
{
    public class CourseService : ICourseService
    {

        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAll() => await _context.Courses.Include(m => m.CourseImages).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Course> GetById(int id) => await _context.Courses.Include(m => m.CourseImages).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Course> GetFullDataById(int id) => await _context.Courses.Include(m => m.CourseImages).FirstOrDefaultAsync(m => m.Id == id);

    }
}
