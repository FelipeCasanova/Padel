using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Transform;
using Padel.Domain;
using Padel.Web.Mvc.Controllers.ViewModels;
using SharpArch.NHibernate;
using Padel.Web.Mvc.Controllers.ViewModels.Torneos;

namespace Padel.Web.Mvc.Controllers.Queries.Torneos
{
    public class TorneosQuery : NHibernateQuery, ITorneosQuery
    {
        public IList<TorneoViewModel> GetTorneosPendientesList()
        {
            var query = Session.QueryOver<Torneo>().OrderBy(x => x.Nombre).Asc
                .Where(t => t.Tipo != TipoTorneoEnum.Privado);

            TorneoViewModel viewModel = null;
            Categoria categoria = null;
            Equipo equipo = null;

            var viewModels =
               query.JoinAlias(x => x.Categorias, () => categoria)
                    //.JoinAlias(() => categoria.Equipos, () => equipo)
                    .Where(x => categoria.Estado != EstadoCategoriaEnum.Finalizado)
                    .SelectList(list => list
                                         .Select(x => x.Id).WithAlias(() => viewModel.Id)
                                         .Select(x => x.Nombre).WithAlias(() => viewModel.Nombre)
                                         .Select(x => x.Tipo).WithAlias(() => viewModel.Tipo)
                                         
                                         // Flattening the object graph
                                         .Select(x => categoria.Nombre).WithAlias(() => viewModel.Categoria)
                                         .Select(x => categoria.Estado).WithAlias(() => viewModel.EstadoCategoria)
                                         .Select(x => categoria.Precio).WithAlias(() => viewModel.Precio)
                                         .Select(x => categoria.TipoEquipo).WithAlias(() => viewModel.TipoEquipo)
                                         .Select(x => categoria.NivelMin).WithAlias(() => viewModel.NivelMin)
                                         .Select(x => categoria.NivelMax).WithAlias(() => viewModel.NivelMax)

                                         //.SelectGroup(x => x)
                                         //.SelectCount(x => equipoToCategoria.Id).WithAlias(() => viewModel.NumeroEquipos)
                                         )

                .TransformUsing(Transformers.AliasToBean(typeof(TorneoViewModel)))
                .Future<TorneoViewModel>();

            return new List<TorneoViewModel>(viewModels);
        }

    }
}