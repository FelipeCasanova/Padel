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
using NHibernate;

namespace Padel.Web.Mvc.Controllers.Queries.Torneos
{
    public class TorneosQuery : NHibernateQuery, ITorneosQuery
    {

        public IList<EquipoTorneoViewModel> GetTorneosPorJugadorNotStatusList(int idJugador, string tipo, params EstadoCategoriaEnum[] estados)
        {
            EquipoTorneoViewModel viewModel = null;
            Categoria categoria = null;
            EquipoToCategoria equipoToCategoria = null;
            Equipo equipo = null;

            var subQuery = QueryOver.Of<Categoria>()
                .JoinAlias(c => c.EquiposToCategorias, () => equipoToCategoria)
                .Where(c => equipoToCategoria.Categoria.Id == categoria.Id && equipoToCategoria.Estado != EstadoEquipoCategoriaEnum.Eliminado)
                .ToRowCountQuery();

            var subQueryJugadorA = QueryOver.Of<Usuario>()
                .Where(u => equipo.JugadorA.Id == u.Id)
                .SelectList(list => list.
                    Select(u => u.Nombre)
                    );
            var subQueryJugadorB = QueryOver.Of<Usuario>()
                .Where(u => equipo.JugadorB.Id == u.Id)
                .SelectList(list => list.
                    Select(u => u.Nombre)
                    );

            var query = Session.QueryOver<Torneo>().OrderBy(x => x.Nombre).Asc;
            // Join with categories
            query = query.JoinAlias(t => t.Categorias, () => categoria);
            // Join with teamsCategories and categories
            query = query.JoinAlias(t => categoria.EquiposToCategorias, () => equipoToCategoria);
            // Join with teams and teamsCategories
            query = (IQueryOver<Torneo, Torneo>)query.JoinAlias(t => equipoToCategoria.Equipo, () => equipo);

            // Filtrar por tipo de torneo
            if (!string.IsNullOrEmpty(tipo))
            {
                query = query.Where(t => t.Tipo == (TipoTorneoEnum)Enum.Parse(typeof(TipoTorneoEnum), tipo));
            }

            // Filtrar por estado de la categoria
            foreach (var estado in estados)
            {
                query = query.Where(x => categoria.Estado != estado);
            }

            // Solo para el jugador y para equipocategorias no eliminadas
            query = query.Where(x => (equipo.JugadorA.Id == idJugador || equipo.JugadorB.Id == idJugador) && equipoToCategoria.Estado != EstadoEquipoCategoriaEnum.Eliminado);

            var viewModels = query.SelectList(list => list
                                    .Select(x => x.Id).WithAlias(() => viewModel.Id)
                                    .Select(x => x.Nombre).WithAlias(() => viewModel.Nombre)
                                    .Select(x => x.Tipo).WithAlias(() => viewModel.Tipo)

                                    .Select(x => equipoToCategoria.Id).WithAlias(() => viewModel.EquipoCategoriaId)
                                    .Select(x => equipoToCategoria.DineroFicticioJugadorA).WithAlias(() => viewModel.DineroFicticioJugadorA)
                                    .Select(x => equipoToCategoria.DineroRealJugadorA).WithAlias(() => viewModel.DineroRealJugadorA)
                                    .Select(x => equipoToCategoria.DineroFicticioJugadorB).WithAlias(() => viewModel.DineroFicticioJugadorB)
                                    .Select(x => equipoToCategoria.DineroRealJugadorB).WithAlias(() => viewModel.DineroRealJugadorB)

                                    .Select(x => categoria.Id).WithAlias(() => viewModel.CategoriaId)
                                    .Select(x => categoria.Nombre).WithAlias(() => viewModel.CategoriaNombre)
                                    .Select(x => categoria.Estado).WithAlias(() => viewModel.EstadoCategoria)
                                    .Select(x => categoria.Precio).WithAlias(() => viewModel.Precio)
                                    .Select(x => categoria.TipoEquipo).WithAlias(() => viewModel.TipoEquipo)
                                    .Select(x => categoria.NivelMin).WithAlias(() => viewModel.NivelMin)
                                    .Select(x => categoria.NivelMax).WithAlias(() => viewModel.NivelMax)

                                    .Select(x => equipo.Id).WithAlias(() => viewModel.EquipoId)
                                    .Select(x => equipo.Nombre).WithAlias(() => viewModel.EquipoNombre)
                                    .Select(x => equipo.JugadorA.Id).WithAlias(() => viewModel.JugadorAId)
                                    .Select(x => equipo.JugadorB.Id).WithAlias(() => viewModel.JugadorBId)

                                    .SelectSubQuery(subQuery).WithAlias(() => viewModel.NumeroEquipos)
                                    .SelectSubQuery(subQueryJugadorA).WithAlias(() => viewModel.JugadorANombre)
                                    .SelectSubQuery(subQueryJugadorB).WithAlias(() => viewModel.JugadorBNombre)
                                    )

           .TransformUsing(Transformers.AliasToBean(typeof(EquipoTorneoViewModel)))
           .Future<EquipoTorneoViewModel>();

            return new List<EquipoTorneoViewModel>(viewModels);
        }

        
        public IList<TorneoViewModel> GetPublicTorneosNotStatusList(params EstadoCategoriaEnum[] estados)
        {
            TorneoViewModel viewModel = null;
            Categoria categoria = null;
            EquipoToCategoria equipoToCategoria = null;

            var query = Session.QueryOver<Torneo>().OrderBy(x => x.Nombre).Asc
                .Where(t => t.Tipo != TipoTorneoEnum.Privado);

            var subQuery = QueryOver.Of<Categoria>()
                .JoinAlias(c => c.EquiposToCategorias, () => equipoToCategoria)
                .Where(c => equipoToCategoria.Categoria.Id == categoria.Id && equipoToCategoria.Estado != EstadoEquipoCategoriaEnum.Eliminado)
                .ToRowCountQuery();

            query = query.JoinAlias(x => x.Categorias, () => categoria);
            foreach (var estado in estados)
            {
                query = query.Where(x => categoria.Estado != estado);
            }

            var viewModels = query.SelectList(list => list
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