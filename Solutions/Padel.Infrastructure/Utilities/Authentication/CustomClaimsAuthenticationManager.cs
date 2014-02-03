using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Claims;

namespace Padel.Infrastructure.Utilities.Authentication
{
    public class CustomClaimsAuthenticationManager : ClaimsAuthenticationManager
    {
        public override IClaimsPrincipal Authenticate(string resourceName, IClaimsPrincipal incomingPrincipal)
        {
            IClaimsPrincipal principal = base.Authenticate(resourceName, incomingPrincipal);
            return new PadelPrincipal(principal);
        }
    }
}
