using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Claims;
using System.ComponentModel.DataAnnotations;

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

        public string Sexo
        {
            get
            {
                return ((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == "http://http://flipersanvi.no-ip.biz/accesscontrolservice/2014/01/claims/gender").Value;
            }
        }

        [UIHint("GameExp")]
        public int Experiencia
        {
            get
            {
                return int.Parse(((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == "http://http://flipersanvi.no-ip.biz/accesscontrolservice/2014/01/claims/exp").Value);
            }
        }

        [UIHint("GameLevel")]
        public int Nivel
        {
            get
            {
                return int.Parse(((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == "http://http://flipersanvi.no-ip.biz/accesscontrolservice/2014/01/claims/level").Value);
            }
        }

        [UIHint("GameHearts")]
        public int Corazones
        {
            get
            {
                return int.Parse(((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == "http://http://flipersanvi.no-ip.biz/accesscontrolservice/2014/01/claims/hearts").Value);
            }
        }

        [UIHint("GameCurrency")]
        public decimal Puntos
        {
            get
            {
                return decimal.Parse(((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == "http://http://flipersanvi.no-ip.biz/accesscontrolservice/2014/01/claims/points").Value);
            }
        }

        public DateTime FechaCreacion
        {
            get
            {
                return DateTime.Parse(((IClaimsIdentity)Identity).Claims.First(c => c.ClaimType == "http://http://flipersanvi.no-ip.biz/accesscontrolservice/2014/01/claims/creationdate").Value);
            }
        }

    }
}
