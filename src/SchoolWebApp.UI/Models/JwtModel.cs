using System;

namespace SchoolWebApp.UI.Models
{
    public class JwtModel
    {
        public bool HasVerifiedEMail { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public int StudentId { get; set; }
        public DateTime Expiration { get; set; }
    }
}
