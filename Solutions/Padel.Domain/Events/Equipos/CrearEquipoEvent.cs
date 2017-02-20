using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace Padel.Domain.Events.Equipos
{
    public class CrearEquipoEvent : INotification
    {
        public CrearEquipoEvent(int equipoId, int jugador1Id, int jugador2Id)   
        {
            this.EquipoId = equipoId;
            this.Jugador1Id = jugador1Id;
            this.Jugador2Id = jugador2Id;
        }
        
        public int EquipoId { get; set; }

        public int Jugador1Id { get; set; }

        public int Jugador2Id { get; set; }
    }
}
