using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;

namespace Padel.Tasks.Events.Usuarios
{
    public class RegistrarEvent : IDomainEvent
    {
        public RegistrarEvent(int usuarioId)
        {
            this.UsuarioId = usuarioId;
        }

        public int UsuarioId { get; set; }
    }
}
