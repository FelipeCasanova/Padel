using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace Padel.Domain.Events.Usuarios
{
    public class IngresarCorazonesEvent : INotification
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
