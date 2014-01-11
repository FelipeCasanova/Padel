using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using NHibernate.Transform;
using Padel.Domain;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels;
using SharpArch.NHibernate;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Torneos
{
    public class TorneosQuery : NHibernateQuery, ITorneosQuery
    {
        public IPagination<TorneoViewModel> GetTorneosList(int page, int size)
        {
            var query = Session.QueryOver<Torneo>().OrderBy(x => x.Nombre).Asc;

            var count = query.ToRowCountQuery();
            var totalCount = count.FutureValue<int>();

            var firstResult = (page - 1) * size;

            TorneoViewModel viewModel = null;
            Categoria categoria = null;

            var viewModels =
               query.JoinAlias(x => x.Categorias, () => categoria)
                    .SelectList(list => list
                                         .Select(x => x.Id).WithAlias(() => viewModel.Id)
                                         .Select(x => x.Nombre).WithAlias(() => viewModel.Nombre)
                                         .Select(x => x.Tipo).WithAlias(() => viewModel.Tipo)
                                         
                                         // Flattening the object graph
                                         .Select(x => categoria.Nombre).WithAlias(() => viewModel.Categoria))
                .TransformUsing(Transformers.AliasToBean(typeof(TorneoViewModel)))
                .Skip(firstResult)
                .Take(size)
                .Future<TorneoViewModel>();

            return new CustomPagination<TorneoViewModel>(viewModels, page, size, totalCount.Value);
        }
    }
}