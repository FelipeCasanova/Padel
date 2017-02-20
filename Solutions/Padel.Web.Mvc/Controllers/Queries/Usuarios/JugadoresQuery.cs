using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using Padel.Web.Mvc.Controllers.ViewModels.Equipos;

using Padel.Domain;
using SharpArch.NHibernate;
using NHibernate.Transform;
using Padel.Web.Mvc.Controllers.ViewModels.Usuarios;
using NHibernate.Criterion;
using System.Threading;
using Padel.Infrastructure.Utilities;
using NHibernate;

namespace Padel.Web.Mvc.Controllers.Queries.Usuarios
{
    public class JugadoresQuery : NHibernateQuery, IJugadoresQuery
    {
        public JugadoresQuery(ISession session) : base(session)
        {
        }

        public IList<JugadorViewModel> GetJugadorPorNombreList(string nombreJugador)
        {
            JugadorViewModel jugadorViewModel = null;
            var usuario = (PadelPrincipal)Thread.CurrentPrincipal;

            var query = Session.QueryOver<Usuario>().OrderBy(x => x.Nombre).Asc;

            var viewModels = query
                .WhereNot(u => u.Id == usuario.Id)
                .WhereRestrictionOn(u => u.Nombre)
                .IsLike(nombreJugador, MatchMode.Anywhere)
                .SelectList(list => list
                        .Select(j => j.Id).WithAlias(() => jugadorViewModel.Id)
                        .Select(j => j.Nombre).WithAlias(() => jugadorViewModel.Nombre)
                        
                    )
                .TransformUsing(Transformers.AliasToBean(typeof(JugadorViewModel)))
                .Future<JugadorViewModel>();

            return viewModels.ToList();
        }
    }
}