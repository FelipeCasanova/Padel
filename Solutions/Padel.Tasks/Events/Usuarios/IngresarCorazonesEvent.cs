using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;

namespace Padel.Tasks.Events.Usuarios
{
    public class IngresarCorazonesEvent : IDomainEvent
    {
        public IngresarCorazonesEvent(int usuarioId, int cantidadPuntos, string accion)
        {
            this.UsuarioId = usuarioId;
            this.CantidadPuntos = cantidadPuntos;
            this.Accion = accion;
        }

        public int CantidadPuntos { get; set; }

        public int UsuarioId { get; set; }

        public string Accion { get; set; }
    }
}
