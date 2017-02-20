using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace Padel.Domain.Events.Usuarios
{
    public class RefrescarUsuarioEvent : INotification
    {
        public RefrescarUsuarioEvent()
        {
        }
    }
}
