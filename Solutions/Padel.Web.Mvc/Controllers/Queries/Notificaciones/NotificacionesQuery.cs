using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using MvcContrib.Pagination;
using NHibernate.Criterion;
using NHibernate.Transform;
using Padel.Domain;
using Padel.Domain.Operaciones;
using Padel.Infrastructure.Utilities;
using Padel.Web.Mvc.Controllers.ViewModels.Equipos;
using Padel.Web.Mvc.Controllers.ViewModels.Operaciones;
using Padel.Web.Mvc.Controllers.ViewModels.Usuarios;
using SharpArch.NHibernate;
using Padel.Web.Mvc.Controllers.ViewModels.Notificaciones;
using Padel.Domain.Notificaciones;
using NHibernate;

namespace Padel.Web.Mvc.Controllers.Queries.Notificaciones
{
    public class NotificacionesQuery : NHibernateQuery, INotificacionesQuery
    {
        public NotificacionesQuery(ISession session) : base(session)
        {
        }

        public IList<NotificacionModelView> GetNotificacionesPorUsuario(int usuarioId)
        {
            var query = Session.QueryOver<Notificacion>().OrderBy(x => x.FechaCreacion).Desc;

            var models = query
                .Where(o => o.Usuario.Id == usuarioId)
                .Future<Notificacion>();

            return models.ToList().Select<dynamic, NotificacionModelView>(o => NotificacionModelView.Crear(o)).ToList();
        }

    }
}