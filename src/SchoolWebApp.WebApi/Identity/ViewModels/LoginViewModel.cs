using System.ComponentModel.DataAnnotations;

namespace SchoolWebApp.WebApi.Identity.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
