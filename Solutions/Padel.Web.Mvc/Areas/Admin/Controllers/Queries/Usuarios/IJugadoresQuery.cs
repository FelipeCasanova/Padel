using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using MvcContrib.Pagination;
using Padel.Web.Mvc.Controllers.ViewModels;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Usuarios
{
    public interface IJugadoresQuery
    {
        IPagination<JugadorViewModel> GetJugadoresList(int page, int size);
    }
}