using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using MvcContrib.Pagination;
using Padel.Web.Mvc.Controllers.ViewModels;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Torneos
{
    public interface ITorneosQuery
    {
        IPagination<TorneoViewModel> GetTorneosList(int page, int size);
    }
}