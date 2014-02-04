using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Transform;
using Padel.Domain;
using Padel.Web.Mvc.Controllers.ViewModels;
using SharpArch.NHibernate;
using Padel.Web.Mvc.Controllers.ViewModels.Torneos;
using NHibernate.Criterion;

namespace Padel.Web.Mvc.Controllers.Queries.Torneos
{
    public class TorneosQuery : NHibernateQuery, ITorneosQuery
    {
        public IList<TorneoViewModel> GetTorneosPendientesList()
        {
            TorneoViewModel viewModel = null;
            Categoria categoria = null;
            EquipoToCategoria equiposToCategorias = null;

            var query = Session.QueryOver<Torneo>().OrderBy(x => x.Nombre).Asc
                .Where(t => t.Tipo != TipoTorneoEnum.Privado);

            var subQuery = QueryOver.Of<Categoria>()
                .JoinAlias(c => c.EquiposToCategorias, () => equiposToCategorias)
                .Where(c => equiposToCategorias.Categoria.Id == categoria.Id)
                .ToRowCountQuery();

            var viewModels =
               query.JoinAlias(x => x.Categorias, () => categoria)
                    .Where(x => categoria.Estado != EstadoCategoriaEnum.Finalizado)
                    .SelectList(list => list
                                         .Select(x => x.Id).WithAlias(() => viewModel.Id)
                                         .Select(x => x.Nombre).WithAlias(() => viewModel.Nombre)
                                         .Select(x => x.Tipo).WithAlias(() => viewModel.Tipo)
                                         
                                         // Flattening the object graph
                                         .Select(x => categoria.Id).WithAlias(() => viewModel.CategoriaId)
                                         .Select(x => categoria.Nombre).WithAlias(() => viewModel.CategoriaNombre)
                                         .Select(x => categoria.Estado).WithAlias(() => viewModel.EstadoCategoria)
                                         .Select(x => categoria.Precio).WithAlias(() => viewModel.Precio)
                                         .Select(x => categoria.TipoEquipo).WithAlias(() => viewModel.TipoEquipo)
                                         .Select(x => categoria.NivelMin).WithAlias(() => viewModel.NivelMin)
                                         .Select(x => categoria.NivelMax).WithAlias(() => viewModel.NivelMax)

                                         .SelectSubQuery(subQuery).WithAlias(() => viewModel.NumeroEquipos)
                                         )

                .TransformUsing(Transformers.AliasToBean(typeof(TorneoViewModel)))
                .Future<TorneoViewModel>();

            return new List<TorneoViewModel>(viewModels);
        }

    }
}