using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands.Usuarios
{
    public class RefrescarUsuarioCommand : CommandBase
    {
        public RefrescarUsuarioCommand(int? telefonoMovil = null)
        {
            this.TelefonoMovil = telefonoMovil;
        }

        public int? TelefonoMovil { get; set; }
    }
}
