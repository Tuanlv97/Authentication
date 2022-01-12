using Microsoft.AspNetCore.Identity;
using System;

namespace BookStore.IdentityProvider
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CompanyStartedDate { get; set; }

        public string Address { get; set; }
    }
}
