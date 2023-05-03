using Microsoft.AspNetCore.Identity;

namespace Entity_Framework_Slider.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
