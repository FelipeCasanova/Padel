using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;

namespace Padel.Tasks.Commands.Usuarios
{
    public class RegistrarUsuarioCommand : UsuarioCommandBase
    {
        public RegistrarUsuarioCommand(string nombre, SexoEnum sexo, int telefonoMovil, string email, string password, string ip) : base(nombre, sexo, telefonoMovil, email)
        {
            this.Password = password;
            this.IP = ip;

        }

        public string Password { get; set; }

        public string IP { get; set; }
    }
}
