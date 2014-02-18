﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands.Torneos
{
    public class EliminarEquipoDeTorneoCommand : CommandBase
    {
        public EliminarEquipoDeTorneoCommand(int idEquipoCategoria)
        {
            this.EquipoCategoriaId = idEquipoCategoria;
        }

        public int EquipoCategoriaId { get; set; }
    }
}
