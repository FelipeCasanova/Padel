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
            if (model is DineroNotificacion)
                return DineroNotificacionModelView.Crear((DineroNotificacion)model);

            return new NotificacionModelView(model);
        }


        public string Accion { get; set; }

        public string Mensaje { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}