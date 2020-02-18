using Models.Identity;
using System;

namespace SchoolWebApp.WebApi.Identity.Models
{
    public class TokenModel
    {
        public bool? HasVerifiedEmail { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
        public bool? IsAdmin { get; set; }
        public ApplicationUser User { get; set; }
    }
}
