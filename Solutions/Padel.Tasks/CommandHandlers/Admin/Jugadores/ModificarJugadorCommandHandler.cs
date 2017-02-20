using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands.Admin.Jugadores;
using Padel.Domain.Contracts.Tasks;
using Padel.Domain;
using MediatR;
using Padel.Domain.Events.Admin.Jugadores;
using System.ComponentModel.DataAnnotations;

namespace Padel.Tasks.CommandHandlers.Admin.Jugadores
{
    public class ModificarJugadorCommandHandler : IRequestHandler<ModificarJugadorCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;
        private readonly IMediator mediator;

        public ModificarJugadorCommandHandler(IUsuarioTasks usuarioTasks, IMediator mediator)
        {
            this.usuarioTasks = usuarioTasks;
            this.mediator = mediator;
        }


        public CommandResult Handle(ModificarJugadorCommand command)
        {
            Usuario usuario = usuarioTasks.Get(command.Id);
            usuario.Nombre = command.Nombre;
            usuario.Sexo = command.Sexo;
            usuario.TelefonoMovil = command.TelefonoMovil;
            usuario.Email = command.Email;
            var validatorCtx = new ValidationContext(usuario);
            if (usuario.IsValid(validatorCtx))
            {
                this.usuarioTasks.CreateOrUpdate(usuario);
                mediator.Publish(new ModificarJugadorEvent(usuario.Id, usuario.Nombre, usuario.Sexo, usuario.TelefonoMovil, usuario.Email));
                return new CommandResult(true, "El usuario " + usuario.Nombre + " ha sido modificado correctamente.");
            }
            else
            {
                return new CommandResult(false, "There was a problem modifying this user: " + usuario.Nombre);
            }
        }
    }
}
