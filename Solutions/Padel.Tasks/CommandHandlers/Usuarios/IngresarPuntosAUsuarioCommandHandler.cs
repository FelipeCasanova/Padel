using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Tasks.Commands.Usuarios;
using Padel.Tasks.CommandResults;
using Padel.Domain.Contracts.Tasks;
using Padel.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Padel.Domain.Events.Usuarios;

namespace Padel.Tasks.CommandHandlers.Usuarios
{
    public class IngresarPuntosAUsuarioCommandHandler : IRequestHandler<IngresarPuntosAUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;
        private readonly IMediator mediator;

        public IngresarPuntosAUsuarioCommandHandler(IUsuarioTasks usuarioTasks, IMediator mediator)
        {
            this.usuarioTasks = usuarioTasks;
            this.mediator = mediator;
        }

        public CommandResult Handle(IngresarPuntosAUsuarioCommand command)
        {
            Usuario usuario = this.usuarioTasks.Get(command.UsuarioId);
            var validatorCtx = new ValidationContext(usuario);
            if (usuario.IsValid(validatorCtx))
            {
                usuario.DineroFicticio += command.CantidadPuntos;
                this.usuarioTasks.CreateOrUpdate(usuario);
                mediator.Publish(new IngresarDineroFicticioEvent(usuario.Id, command.CantidadPuntos));
                mediator.Publish(new RefrescarUsuarioEvent());
                return new CommandResult(true, string.Empty);
            }
            return new CommandResult(false, string.Empty);
        }
    }
}
