using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using NHibernate.Criterion;
using NHibernate.Transform;
using Padel.Domain;
using Padel.Infrastructure.Utilities.Extensions;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Torneos;
using SharpArch.NHibernate;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Torneos
{
    public class TorneosQuery : NHibernateQuery, ITorneosQuery
    {
        public IPagination<TorneoViewModel> GetTorneosList(int page, int size, params EstadoCategoriaEnum[] estados)
        {
            var query = Session.QueryOver<Torneo>().OrderBy(x => x.Nombre).Asc;

            var count = query.ToRowCountQuery();
            var totalCount = count.FutureValue<int>();

            var firstResult = (page - 1) * size;

            TorneoViewModel viewModel = null;
            Categoria categoria = null;

            query = query.JoinAlias(x => x.Categorias, () => categoria);

            // Filtrar por estado de la categoria
            var criterios = new List<ICriterion>();
            foreach (var estado in estados)
            {
                criterios.Add(Restrictions.On<Torneo>(t => categoria.Estado).IsLike(estado));
            }
            query = query.Or(criterios.ToArray());

            var viewModels = query    
                    .SelectList(list => list
                                         .Select(x => x.Id).WithAlias(() => viewModel.Id)
                                         .Select(x => x.Nombre).WithAlias(() => viewModel.Nombre)
                                         .Select(x => x.Tipo).WithAlias(() => viewModel.Tipo)
                                         .Select(x => categoria.Id).WithAlias(() => viewModel.CategoriaId)

                                         // Flattening the object graph
                                         .Select(x => categoria.Nombre).WithAlias(() => viewModel.Categoria)
                                         .Select(x => categoria.Estado).WithAlias(() => viewModel.Estado)
                                         .Select(x => categoria.FechaModificacion).WithAlias(() => viewModel.FechaModificacion)
                                         )
                .TransformUsing(Transformers.AliasToBean(typeof(TorneoViewModel)))
                .Skip(firstResult)
                .Take(size)
                .Future<TorneoViewModel>();

            return new CustomPagination<TorneoViewModel>(viewModels, page, size, totalCount.Value);
        }
    }
}