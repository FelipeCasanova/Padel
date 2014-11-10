using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Padel.Domain.Notificaciones
{

    public class CorazonNotificacion : Notificacion
    {
        public enum AccionEnum : byte { IngresoCorazonesRegistro = 0, IngresoCorazonesActualizado = 1 }

        public virtual decimal CantidadPuntos { get; set; }
    }
}
