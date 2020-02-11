using Microsoft.AspNetCore.Identity;

namespace WebApi.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public int? StudentId { get; set; }
    }
}
