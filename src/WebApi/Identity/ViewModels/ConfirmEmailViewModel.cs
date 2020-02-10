using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Identity.ViewModels
{
    public class ConfirmEmailViewModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
