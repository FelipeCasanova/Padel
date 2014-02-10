using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using Padel.Web.Mvc.Controllers.ViewModels.Equipos;
using Padel.Domain;

namespace Padel.Web.Mvc.Controllers.Queries.Equipos
{
    public interface IEquiposQuery
    {
        IList<EquipoViewModel> GetEquiposPorJugadorList(int idJugador, string tipo);
        IList<EquipoViewModel> GetEquiposPorJugadoresList(int idJugadorA, int idJugadorB);
    }
}