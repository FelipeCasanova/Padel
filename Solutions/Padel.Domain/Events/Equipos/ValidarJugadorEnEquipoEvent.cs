using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Domain.Events.Equipos
{
    public class ValidarJugadorEnEquipoEvent : INotification
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
