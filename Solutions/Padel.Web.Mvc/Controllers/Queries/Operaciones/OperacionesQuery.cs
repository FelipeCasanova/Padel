using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using MvcContrib.Pagination;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Padel.Domain;
using Padel.Domain.Operaciones;
using Padel.Infrastructure.Utilities;
using Padel.Web.Mvc.Controllers.ViewModels.Equipos;
using Padel.Web.Mvc.Controllers.ViewModels.Operaciones;
using Padel.Web.Mvc.Controllers.ViewModels.Usuarios;
using SharpArch.NHibernate;

namespace Padel.Web.Mvc.Controllers.Queries.Operaciones
{
    public class OperacionesQuery : NHibernateQuery, IOperacionesQuery
    {
        public OperacionesQuery(ISession session) : base(session)
        {
        }

        public IList<OperacionModelView> GetOperacionesPorUsuario(int usuarioId)
        {
            var query = Session.QueryOver<Operacion>().OrderBy(x => x.FechaCreacion).Desc;

            var models = query
                .Where(o => o.Usuario.Id == usuarioId)
                .Future<Operacion>();

            return models.ToList().Select<Operacion, OperacionModelView>(o => OperacionModelView.Crear(o)).ToList();
        }

    }
}