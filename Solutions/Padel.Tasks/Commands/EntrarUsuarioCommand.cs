using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands
{
    public class EntrarUsuarioCommand : CommandBase
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
