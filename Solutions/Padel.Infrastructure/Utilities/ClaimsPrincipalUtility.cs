using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Claims;
using Padel.Domain;
using System.Threading;
using System.Web;

namespace Padel.Infrastructure.Utilities
{
    public class ClaimsPrincipalUtility
    {
        private static readonly string raiz = System.Web.Configuration.WebConfigurationManager.AppSettings["RaizPadelApp"];

        public static IClaimsPrincipal CreatePrincipal(Usuario usuario)
        { 
            // Listado de claims
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "padel"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.TelefonoMovil.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, usuario.Nombre));
            claims.Add(new Claim(ClaimTypes.Role, usuario.Roles.First().Nombre));
            claims.Add(new Claim(ClaimTypes.PPID, usuario.Id.ToString()));
            claims.Add(new Claim("http://" + raiz + "/accesscontrolservice/2014/01/claims/gender", usuario.Sexo.ToString()));
            claims.Add(new Claim("http://" + raiz + "/accesscontrolservice/2014/01/claims/exp", usuario.PuntosExperiencia.ToString()));
            claims.Add(new Claim("http://" + raiz + "/accesscontrolservice/2014/01/claims/level", usuario.Nivel.ToString()));
            claims.Add(new Claim("http://" + raiz + "/accesscontrolservice/2014/01/claims/hearts", usuario.AplicacionExperiencia.ToString()));
            claims.Add(new Claim("http://" + raiz + "/accesscontrolservice/2014/01/claims/points", usuario.DineroFicticio.ToString()));

            if (!string.IsNullOrEmpty(usuario.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
            }
            claims.Add(new Claim("http://" + raiz + "/accesscontrolservice/2014/01/claims/creationdate", usuario.FechaCreacion.ToShortDateString()));

            // Creando la identidad para el principal
            IList<IClaimsIdentity> identities = new List<IClaimsIdentity>();
            IClaimsIdentity identity = new ClaimsIdentity(claims, "Forms");
            identities.Add(identity);

            // Creando el principal
            IClaimsPrincipal principal = new PadelPrincipal(new ClaimsPrincipal(identities));
            HttpContext.Current.User = Thread.CurrentPrincipal = principal;

            return principal;
        }
    }
}
