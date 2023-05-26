using System.Security.Claims;

namespace Identity.Models
{
    public static class ClaimStore
    {
        public static List<Claim> claimList = new List<Claim>()
       {
           new Claim ("Create", "Create"),
           new Claim ("Edit", "Delete"),
           new Claim ("Delete", "Delete")
       };
    }
}
