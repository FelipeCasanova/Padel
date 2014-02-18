using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;

namespace Padel.Tasks.Events
{
    public class RegistroEvent : IDomainEvent
    {
        public RegistroEvent(int usuarioId)
        {
            this.UsuarioId = usuarioId;
        }

        public int UsuarioId { get; set; }
    }
}
