using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace Padel.Domain.Events.Usuarios
{
    public class RegistrarEvent : INotification
    {
        public RegistrarEvent(int usuarioId)
        {
            this.UsuarioId = usuarioId;
        }

        public int UsuarioId { get; set; }
    }
}
