using System.ComponentModel.DataAnnotations;

namespace eTicketing.Data.ViewModel
{
    public class RegisterVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage ="confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password Did Not Match")]
        public string confirmPassword { get; set; }
    }
}
