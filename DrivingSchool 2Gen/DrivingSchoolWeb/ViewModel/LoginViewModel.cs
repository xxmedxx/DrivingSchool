using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolWeb.ViewModel
{
    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name= "Email Address")]
        public string Email { get; set; }

        [Required,DataType(DataType.Password),Display(Name ="Password")]
        public string Password { get; set; }

    }
}
