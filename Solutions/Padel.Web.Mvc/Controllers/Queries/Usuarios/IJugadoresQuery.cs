using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using Padel.Web.Mvc.Controllers.ViewModels.Equipos;
using Padel.Web.Mvc.Controllers.ViewModels.Usuarios;

namespace Padel.Web.Mvc.Controllers.Queries.Equipos
{
    public interface IJugadoresQuery
    {
        IList<JugadorViewModel> GetJugadorPorNombreList(string nombreJugador);
    }
}