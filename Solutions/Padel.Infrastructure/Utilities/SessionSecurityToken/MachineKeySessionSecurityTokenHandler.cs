using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Web;

namespace Padel.Infrastructure.Utilities.SessionSecurityToken
{

    /// <summary>
    /// This class encrypts the session security token using the ASP.NET configured machine key.
    /// </summary>
    public class MachineKeySessionSecurityTokenHandler : SessionSecurityTokenHandler
    {
        static List<CookieTransform> _transforms;

        static MachineKeySessionSecurityTokenHandler()
        {
            _transforms = new List<CookieTransform>() 
                        { 
                            new DeflateCookieTransform(), 
                            new MachineKeyProtectionTransform()
                        };
        }

        public MachineKeySessionSecurityTokenHandler()
            : base(_transforms.AsReadOnly())
        {
        }
    }

}
