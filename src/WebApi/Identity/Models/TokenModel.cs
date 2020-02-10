using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Identity.Models
{
    public class TokenModel
    {
        public bool? HasVerifiedEmail { get; set; }
        public bool? TFAEnabled { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
