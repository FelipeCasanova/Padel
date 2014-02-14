using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands.Equipos
{
    public class ValidarJugadorEnEquipoCommand : CommandBase
    {
        public ValidarJugadorEnEquipoCommand(int idEquipo, int idJugador)
        {
            this.IdEquipo = idEquipo;
            this.IdJugador = idJugador;
        }

        public int IdEquipo { get; set; }
    
        public int IdJugador { get; set; }}
}
