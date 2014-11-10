using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;
using Padel.Tasks.Commands.Usuarios;
using Padel.Tasks.CommandResults;
using Padel.Domain.Contracts.Tasks;
using Padel.Domain;
using SharpArch.Domain.Events;
using Padel.Tasks.Events.Usuarios;

namespace Padel.Tasks.CommandHandlers.Usuarios
{
    public class IngresarPuntosAUsuarioCommandHandler : ICommandHandler<IngresarPuntosAUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;

        public IngresarPuntosAUsuarioCommandHandler(IUsuarioTasks usuarioTasks)
        {
            this.usuarioTasks = usuarioTasks;
        }

        public CommandResult Handle(IngresarPuntosAUsuarioCommand command)
        {
            Usuario usuario = this.usuarioTasks.Get(command.UsuarioId);
            if (usuario.IsValid())
            {
                usuario.DineroFicticio += command.CantidadPuntos;
                this.usuarioTasks.CreateOrUpdate(usuario);
                DomainEvents.Raise<IngresarDineroFicticioEvent>(new IngresarDineroFicticioEvent(usuario.Id, command.CantidadPuntos));
                DomainEvents.Raise<RefrescarUsuarioEvent>(new RefrescarUsuarioEvent());
                return new CommandResult(true, string.Empty);
            }
            return new CommandResult(false, string.Empty);
        }
    }
}
