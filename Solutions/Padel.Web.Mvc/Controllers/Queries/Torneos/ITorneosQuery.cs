using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using Padel.Web.Mvc.Controllers.ViewModels.Torneos;

namespace Padel.Web.Mvc.Controllers.Queries.Torneos
{
    public interface ITorneosQuery
    {
        IList<TorneoViewModel> GetTorneosPendientesList();
    }
}