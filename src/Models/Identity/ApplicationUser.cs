using Microsoft.AspNetCore.Identity;

namespace Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int StudentId { get; set; }
    }
}
