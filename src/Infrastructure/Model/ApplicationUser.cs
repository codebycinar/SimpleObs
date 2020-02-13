using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Model
{
    public class ApplicationUser : IdentityUser
    {
        public int StudentId { get; set; }
    }
}
