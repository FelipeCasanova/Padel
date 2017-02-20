using MediatR;
using Padel.Tasks.CommandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Tasks.Commands.Usuarios
{
    public class RefrescarUsuarioCommand : IRequest<CommandResult>
    {
        public RefrescarUsuarioCommand(int? telefonoMovil = null)
        {
            this.TelefonoMovil = telefonoMovil;
        }

        public int? TelefonoMovil { get; set; }
    }
}
