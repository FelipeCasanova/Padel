﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using MediatR;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.Commands.Torneos
{
    public class CrearEquipoParaTorneoCommand : IRequest<CommandResult>
    {
        public CrearEquipoParaTorneoCommand(int torneoId, int categoriaId, int jugador1Id, int equipoId, int jugador2Id)
        {
            this.TorneoId = torneoId;
            this.CategoriaId = categoriaId;
            this.Jugador1Id = jugador1Id;
            this.EquipoId = equipoId;
            this.Jugador2Id = jugador2Id;
        }

        public int TorneoId { get; set; }

        public int CategoriaId { get; set; }

        public int Jugador1Id { get; set; }

        public int EquipoId { get; set; }

        public int Jugador2Id { get; set; }
    }
}
