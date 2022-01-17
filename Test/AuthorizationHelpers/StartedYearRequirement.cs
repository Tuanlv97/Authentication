using Microsoft.AspNetCore.Authorization;

namespace Test.AuthorizationHelpers
{
    public class StartedYearRequirement : IAuthorizationRequirement
    {
        public StartedYearRequirement(int startedYear)
        {
            StartedYear = startedYear;
        }
        public int StartedYear { get; set; }
    }
}
