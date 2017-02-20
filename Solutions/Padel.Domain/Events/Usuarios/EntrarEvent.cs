using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace Padel.Domain.Events.Usuarios
{
    public class EntrarEvent : INotification
    {
        public EntrarEvent(string emailOrMovil)
        {
            this.EmailOrMovil = emailOrMovil;
        }

        public string EmailOrMovil { get; set; }
    }
}
