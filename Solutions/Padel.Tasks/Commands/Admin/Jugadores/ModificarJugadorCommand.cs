using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;
using Padel.Tasks.Commands.Usuarios;
using Padel.Domain;

namespace Padel.Tasks.Commands.Admin.Jugadores
{
    public class ModificarJugadorCommand : UsuarioCommandBase
    {
        public int Id { get; set; }

        public ModificarJugadorCommand(int id, string nombre, SexoEnum sexo, int telefonoMovil, string email)
            : base(nombre, sexo, telefonoMovil, email)
        {
            this.Id = id;
        }
    }
}
