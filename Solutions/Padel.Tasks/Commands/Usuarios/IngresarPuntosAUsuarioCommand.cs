using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.Commands.Usuarios
{
    public class IngresarPuntosAUsuarioCommand : IRequest<CommandResult>
    {

        public IngresarPuntosAUsuarioCommand(decimal cantidadPuntos, int usuarioId)
        {
            this.CantidadPuntos = cantidadPuntos;
            this.UsuarioId = usuarioId;
        }

        [Range(0,20)]
        public decimal CantidadPuntos { get; set; }

        public int UsuarioId { get; set; }
    }
}
