using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;

namespace Padel.Tasks.Events.Usuarios
{
    public class EntrarEvent : IDomainEvent
    {
        public EntrarEvent(string emailOrMovil)
        {
            this.EmailOrMovil = emailOrMovil;
        }

        public string EmailOrMovil { get; set; }
    }
}
