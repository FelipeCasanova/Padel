using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using NHibernate.Transform;
using Padel.Domain;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels;
using SharpArch.NHibernate;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Usuarios
{
    public class JugadoresQuery : NHibernateQuery, IJugadoresQuery
    {
        public IPagination<JugadorViewModel> GetJugadoresList(int page, int size)
        {
            var query = Session.QueryOver<Usuario>().OrderBy(x => x.Nombre).Asc;

            var count = query.ToRowCountQuery();
            var totalCount = count.FutureValue<int>();

            var firstResult = (page - 1) * size;

            JugadorViewModel viewModel = null;
            Role rol = null;

            var viewModels =
               query.JoinAlias(x => x.Roles, () => rol)
                    .SelectList(list => list
                                         .Select(x => x.Id).WithAlias(() => viewModel.Id)
                                         .Select(x => x.Nombre).WithAlias(() => viewModel.Nombre)
                                         .Select(x => x.Sexo).WithAlias(() => viewModel.Sexo)
                                         .Select(x => x.TelefonoMovil).WithAlias(() => viewModel.TelefonoMovil)
                                         .Select(x => x.Email).WithAlias(() => viewModel.Email)
                                         
                                         // Flattening the object graph
                                         .Select(x => rol.Name).WithAlias(() => viewModel.Role))
                .TransformUsing(Transformers.AliasToBean(typeof(JugadorViewModel)))
                .Skip(firstResult)
                .Take(size)
                .Future<JugadorViewModel>();

            return new CustomPagination<JugadorViewModel>(viewModels, page, size, totalCount.Value);
        }
    }
}