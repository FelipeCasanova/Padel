using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Padel.Domain.Notificaciones
{

    public class DineroNotificacion : Notificacion
    {
        public enum AccionEnum : byte { IngresoPuntosFicticios = 0, IngresoDineroReal = 1 }

        public virtual decimal CantidadPuntos { get; set; }
    }
}
