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
            var query = Session.QueryOver<Torneo>().OrderBy(x => x.Nombre).Asc;

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
                .Future<TorneoViewModel>();

            return new List<TorneoViewModel>(viewModels);
        }

    }
}