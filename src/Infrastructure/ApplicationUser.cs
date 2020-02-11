using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public class ApplicationUser:IdentityUser
    {
        public int StudentId { get; set; }
    }
}
