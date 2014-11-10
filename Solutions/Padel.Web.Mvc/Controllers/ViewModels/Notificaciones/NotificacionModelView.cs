using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain.Notificaciones;

namespace Padel.Web.Mvc.Controllers.ViewModels.Notificaciones
{
    public class NotificacionModelView
    {
        protected NotificacionModelView(Notificacion model)
        {
            Accion = model.Accion;
            Mensaje = model.Mensaje;
            FechaCreacion = model.FechaCreacion;
            FechaModificacion = model.FechaModificacion;
        }

        public static NotificacionModelView Crear(Notificacion model)
        {
            return new NotificacionModelView(model);
        }

        public static NotificacionModelView Crear(DineroNotificacion model)
        {
            return new DineroNotificacionModelView(model);
        }

        public static NotificacionModelView Crear(CorazonNotificacion model)
        {
            return new CorazonNotificacionModelView(model);
        }

        public string Accion { get; set; }

        public string Mensaje { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}