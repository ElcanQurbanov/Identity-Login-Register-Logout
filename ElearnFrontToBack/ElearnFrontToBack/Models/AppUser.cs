using Microsoft.AspNetCore.Identity;

namespace ElearnFrontToBack.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
