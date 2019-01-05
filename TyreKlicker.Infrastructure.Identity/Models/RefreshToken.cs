using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TyreKlicker.Infrastructure.Identity.Models
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string Token { get; set; }

        public string UserId { get; set; }
        public string Email { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}