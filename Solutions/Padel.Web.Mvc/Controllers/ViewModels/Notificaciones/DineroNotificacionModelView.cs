﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain.Notificaciones;

namespace Padel.Web.Mvc.Controllers.ViewModels.Notificaciones
{
    public class DineroNotificacionModelView : NotificacionModelView
    {
        public  DineroNotificacionModelView(DineroNotificacion model)
            : base(model)
        {
        }
    }
}