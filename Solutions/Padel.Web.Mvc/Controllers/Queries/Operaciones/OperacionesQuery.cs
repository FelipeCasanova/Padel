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
using Padel.Web.Mvc.Controllers.ViewModels.Operaciones;
using Padel.Domain.Operaciones;

namespace Padel.Web.Mvc.Controllers.Queries.Operaciones
{
    public class OperacionesQuery : NHibernateQuery, IOperacionesQuery
    {
        public IList<OperacionModelView> GetOperacionesPorUsuario(int usuarioId)
        {
            var query = Session.QueryOver<Operacion>().OrderBy(x => x.FechaCreacion).Desc;

            var models = query
                .Where(o => o.Usuario.Id == usuarioId)
                .Future<Operacion>();

            return models.ToList().Select<Operacion, OperacionModelView>(o => OperacionModelView.Crear(o)).ToList();
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