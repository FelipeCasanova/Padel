using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace Padel.Domain.Events.Usuarios
{
    public class IngresarDineroFicticioEvent : INotification
    {
        public IngresarDineroFicticioEvent(int usuarioId, decimal cantidadPuntos)
        {
            this.UsuarioId = usuarioId;
            this.CantidadPuntos = cantidadPuntos;
        }

        public decimal CantidadPuntos { get; set; }

        public int UsuarioId { get; set; }
    }
}
