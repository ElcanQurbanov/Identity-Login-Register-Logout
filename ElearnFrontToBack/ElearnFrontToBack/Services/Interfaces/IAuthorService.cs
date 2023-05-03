using ElearnFrontToBack.Models;

namespace ElearnFrontToBack.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAll();
    }
}
