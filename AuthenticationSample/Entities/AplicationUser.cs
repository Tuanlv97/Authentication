using Microsoft.AspNetCore.Identity;
using System;

namespace AuthenticationSample
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CompanyStarteDate { get; set; }
        [PersonalData]
        public string Address { get; set; }
    }
}
