using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using MediatR;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.Commands.Usuarios
{
    public class UsuarioCommandBase : IRequest<CommandResult>
    {
        public UsuarioCommandBase(string nombre, SexoEnum sexo, int telefonoMovil, string email)
        {
            this.Nombre = nombre;
            this.Sexo = sexo;
            this.TelefonoMovil = telefonoMovil;
            this.Email = email;
        }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public SexoEnum Sexo { get; set; }

        public int TelefonoMovil { get; set; }

        public string Email { get; set; }

    }
    
}
