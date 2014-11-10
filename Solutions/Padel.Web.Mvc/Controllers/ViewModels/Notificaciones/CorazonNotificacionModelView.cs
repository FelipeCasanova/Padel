using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain.Notificaciones;

namespace Padel.Web.Mvc.Controllers.ViewModels.Notificaciones
{
    public class CorazonNotificacionModelView : NotificacionModelView
    {
        public CorazonNotificacionModelView(CorazonNotificacion model)
            : base(model)
        {
        }
    }
}