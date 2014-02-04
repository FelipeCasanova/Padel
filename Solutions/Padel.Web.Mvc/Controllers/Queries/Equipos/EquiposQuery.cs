using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using Padel.Web.Mvc.Controllers.ViewModels.Equipos;

using Padel.Domain;
using SharpArch.NHibernate;
using NHibernate.Transform;

namespace Padel.Web.Mvc.Controllers.Queries.Equipos
{
    public class EquiposQuery : NHibernateQuery, IEquiposQuery
    {
        public IList<EquipoViewModel> GetEquiposPorJugadorList(int idJugador)
        {
            EquipoViewModel equipoViewModel = null;
            Usuario jugadorA = null;
            Usuario jugadorB = null;

            var query = Session.QueryOver<Equipo>().OrderBy(x => x.Nombre).Asc;

            var viewModels = query.Where(e => (e.JugadorA.Id == idJugador || e.JugadorB.Id == idJugador) && (e.Estado == EstadoEquipoEnum.Activado || e.Estado == EstadoEquipoEnum.Desactivado))
                .JoinAlias(x => x.JugadorA, () => jugadorA)
                .JoinAlias(x => x.JugadorB, () => jugadorB)
                .SelectList(list => list
                        .Select(e => e.Id).WithAlias(() => equipoViewModel.Id)
                        .Select(e => e.Nombre).WithAlias(() => equipoViewModel.Nombre)
                        .Select(e => e.Estado).WithAlias(() => equipoViewModel.Estado)
                        .Select(e => jugadorA.Id).WithAlias(() => equipoViewModel.JugadorAId)
                        .Select(e => jugadorA.Nombre).WithAlias(() => equipoViewModel.JugadorANombre)
                        .Select(e => jugadorB.Id).WithAlias(() => equipoViewModel.JugadorBId)
                        .Select(e => jugadorB.Nombre).WithAlias(() => equipoViewModel.JugadorBNombre)
                        
                    )
                .TransformUsing(Transformers.AliasToBean(typeof(EquipoViewModel)))
                .Future<EquipoViewModel>();

            return viewModels.ToList();
        }


        public IList<EquipoViewModel> GetEquiposPorJugadoresList(int idJugadorA, int idJugadorB)
        {
            EquipoViewModel equipoViewModel = null;
            Usuario jugadorA = null;
            Usuario jugadorB = null;

            var query = Session.QueryOver<Equipo>().OrderBy(x => x.Nombre).Asc;

            var viewModels = query.Where(e => (e.JugadorA.Id == idJugadorA || e.JugadorB.Id == idJugadorA)
                && (e.JugadorA.Id == idJugadorB || e.JugadorB.Id == idJugadorB)
                && (e.Estado == EstadoEquipoEnum.Activado || e.Estado == EstadoEquipoEnum.Desactivado))
                .JoinAlias(x => x.JugadorA, () => jugadorA)
                .JoinAlias(x => x.JugadorB, () => jugadorB)
                .SelectList(list => list
                        .Select(e => e.Id).WithAlias(() => equipoViewModel.Id)
                        .Select(e => e.Nombre).WithAlias(() => equipoViewModel.Nombre)
                        .Select(e => e.Estado).WithAlias(() => equipoViewModel.Estado)
                        .Select(e => jugadorA.Id).WithAlias(() => equipoViewModel.JugadorAId)
                        .Select(e => jugadorA.Nombre).WithAlias(() => equipoViewModel.JugadorANombre)
                        .Select(e => jugadorB.Id).WithAlias(() => equipoViewModel.JugadorBId)
                        .Select(e => jugadorB.Nombre).WithAlias(() => equipoViewModel.JugadorBNombre)
                    )
                .TransformUsing(Transformers.AliasToBean(typeof(EquipoViewModel)))
                .Future<EquipoViewModel>();

            return viewModels.ToList();
        }

    }
}