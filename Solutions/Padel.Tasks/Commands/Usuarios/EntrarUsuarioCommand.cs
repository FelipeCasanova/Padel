using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using MediatR;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.Commands.Usuarios
{
    public class EntrarUsuarioCommand : IRequest<CommandResult>
    {
        public EntrarUsuarioCommand(string emailOrMovil,  string password)
        {
            this.EmailOrMovil = emailOrMovil;
            this.Password = password;
        }

        public string EmailOrMovil { get; set; }

        public string Password { get; set; }

        public int TelefonoMovil;
    }
}
