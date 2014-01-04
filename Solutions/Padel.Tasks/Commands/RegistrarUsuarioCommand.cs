using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;

namespace Padel.Tasks.Commands
{
    public class RegistrarUsuarioCommand : UsuarioCommandBase
    {
        public RegistrarUsuarioCommand(string nombre, SexoEnum sexo, int telefonoMovil, string email, string password) : base(nombre, sexo, telefonoMovil, email)
        {
            this.Password = password;
        }

        public string Password { get; set; }
    }
}
