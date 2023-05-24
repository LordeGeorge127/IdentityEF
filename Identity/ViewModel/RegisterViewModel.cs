using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModel
{
    public class RegisterViewModel
    {
        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="The{0} must be atleast {2} characters Long",MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="The Password and confirmation passwords do not match")]
        public string ConfirmPassword { get; set;}

        public string? ReturnUrl { get; set; }
        public IEnumerable<SelectListItem> ? RoleList { get; set; }
        public string RoleSelected { get; set; }
        
        
    }
}
