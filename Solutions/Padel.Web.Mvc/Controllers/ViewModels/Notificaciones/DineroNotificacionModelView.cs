using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain.Notificaciones;

namespace Padel.Web.Mvc.Controllers.ViewModels.Notificaciones
{
    public class DineroNotificacionModelView : NotificacionModelView
    {
        private DineroNotificacionModelView(DineroNotificacion model)
            : base(model)
        {
        }

        public static DineroNotificacionModelView Crear(DineroNotificacion model)
        {
            return new DineroNotificacionModelView(model);
        }
    }
}