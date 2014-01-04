using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Claims;

namespace Padel.Tasks
{
    public class PadelPrincipal : ClaimsPrincipal
    {
        public PadelPrincipal(IClaimsPrincipal principal)
            : base(principal)
        { 
        }
        
    }
}
