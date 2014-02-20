using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using Padel.Web.Mvc.Controllers.ViewModels.Equipos;
using Padel.Web.Mvc.Controllers.ViewModels.Usuarios;
using Padel.Web.Mvc.Controllers.ViewModels.Operaciones;

namespace Padel.Web.Mvc.Controllers.Queries.Operaciones
{
    public interface IOperacionesQuery
    {
        IList<OperacionModelView> GetOperacionesPorUsuario(int usuarioId);
    }
}