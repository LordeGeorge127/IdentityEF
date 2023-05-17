using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string NickName { get; set; }
        

        
    }
}
