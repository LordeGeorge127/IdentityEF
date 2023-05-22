using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="ConfirmPassword")]
        [Compare("Password",ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set;}
        public string Code { get; set; }

    }
}
