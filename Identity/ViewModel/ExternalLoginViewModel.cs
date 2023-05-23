using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModel
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
       
        public string Name { get; set; }
    }
}
