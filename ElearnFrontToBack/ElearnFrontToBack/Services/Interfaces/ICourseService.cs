using ElearnFrontToBack.Models;

namespace ElearnFrontToBack.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Course> GetById(int id);
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetFullDataById(int id);
    }
}
