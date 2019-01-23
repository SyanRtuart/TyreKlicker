using Microsoft.AspNetCore.Identity;
using System;

namespace TyreKlicker.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid TyreKlickerUserId { get; set; }
    }
}