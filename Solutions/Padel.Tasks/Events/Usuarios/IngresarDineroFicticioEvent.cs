using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;

namespace Padel.Tasks.Events.Usuarios
{
    public class IngresarDineroFicticioEvent : IDomainEvent
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
