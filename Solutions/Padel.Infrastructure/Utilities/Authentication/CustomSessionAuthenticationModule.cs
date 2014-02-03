using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Web;
using System.Threading;
using System.Web;
using Microsoft.IdentityModel.Claims;

namespace Padel.Infrastructure.Utilities.Authentication
{
    public class CustomSessionAuthenticationModule : SessionAuthenticationModule
    {
        protected override void OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            base.OnPostAuthenticateRequest(sender, e);
            if (!(Thread.CurrentPrincipal is PadelPrincipal))
            {
                HttpContext.Current.User = Thread.CurrentPrincipal = new PadelPrincipal((IClaimsPrincipal)Thread.CurrentPrincipal);
            }
        }
    }
}
