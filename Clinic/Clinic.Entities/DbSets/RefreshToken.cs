using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Entities.DbSets
{
    public class RefreshToken : BaseEntity
    {
        public string UserId { get; set; } // User id when logged in
        public string Token { get; set; }
        public string JwtId { get; set; } // The id generated when a jwt id has been requested
        public bool IsUsed { get; set; } // To make sure that the token is only used once
        public bool IsRevoked { get; set; } // To make sure they are valid
        public DateTime ExpiryDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
