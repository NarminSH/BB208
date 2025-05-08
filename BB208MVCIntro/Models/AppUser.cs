using Microsoft.AspNetCore.Identity;

namespace BB208MVCIntro.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
