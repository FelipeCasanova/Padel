using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;

namespace Padel.Tasks.Events.Equipos
{
    public class ValidarJugadorEnEquipoEvent : IDomainEvent
    {
        public ValidarJugadorEnEquipoEvent(int equipoId, int jugador1Id)   
        {
            this.EquipoId = equipoId;
            this.JugadorId = jugador1Id;
        }
        
        public int EquipoId { get; set; }

        public int JugadorId { get; set; }
    }
}
