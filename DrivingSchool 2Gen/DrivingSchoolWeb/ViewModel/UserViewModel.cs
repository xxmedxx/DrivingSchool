using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolWeb.ViewModel
{
    public class UserViewModel
    {
        [Required,EmailAddress,Display(Name ="Email Address")]
        public string Email { get; set; }

        [Required, Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm Password"),Compare("Password",ErrorMessage ="Please make sure passwords are matching")]
        public string ConfirmPassword { get; set; }


    }
}
