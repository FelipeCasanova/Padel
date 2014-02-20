using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;

namespace Padel.Tasks.Events.Equipos
{
    public class CrearEquipoEvent : IDomainEvent
    {
        public CrearEquipoEvent(int usuarioId, int equipoId, int jugador2Id)
        {
            this.UsuarioId = usuarioId;
            this.EquipoId = equipoId;
            this.Jugador2Id = jugador2Id;
        }

        public int UsuarioId { get; set; }

        public int EquipoId { get; set; }

        public int Jugador2Id { get; set; }
    }
}
