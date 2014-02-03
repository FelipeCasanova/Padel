using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Claims;

namespace Padel.Infrastructure.Utilities
{
    public class PadelPrincipal : ClaimsPrincipal
    {
        public PadelPrincipal(IClaimsPrincipal principal)
            : base(principal)
        {
        }

        public int Id
        {
            get
            {
                return int.Parse(((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == ClaimTypes.PPID).Value);
            }
        }

        public string Email
        {
            get
            {
                return ((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == ClaimTypes.Email).Value;
            }
        }

        public DateTime FechaCreacion
        {
            get
            {
                return DateTime.Parse(((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == "http://http://flipersanvi.no-ip.biz//accesscontrolservice/2014/01/claims/creationdate").Value);
            }
        }

    }
}
