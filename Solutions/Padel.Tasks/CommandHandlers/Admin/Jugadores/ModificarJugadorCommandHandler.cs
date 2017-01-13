using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands.Admin.Jugadores;
using Padel.Domain.Contracts.Tasks;
using Padel.Domain;
using SharpArch.Domain.Events;
using Padel.Tasks.Events.Admin.Jugadores;

namespace Padel.Tasks.CommandHandlers.Admin.Jugadores
{
    public class ModificarJugadorCommandHandler : ICommandHandler<ModificarJugadorCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;

        public ModificarJugadorCommandHandler(IUsuarioTasks usuarioTasks)
        {
            this.usuarioTasks = usuarioTasks;   
        }


        public CommandResult Handle(ModificarJugadorCommand command)
        {
            Usuario usuario = usuarioTasks.Get(command.Id);
            usuario.Nombre = command.Nombre;
            usuario.Sexo = command.Sexo;
            usuario.TelefonoMovil = command.TelefonoMovil;
            usuario.Email = command.Email;
            if (usuario.IsValid())
            {
                this.usuarioTasks.CreateOrUpdate(usuario);
                DomainEvents.Raise<ModificarJugadorEvent>(new ModificarJugadorEvent(usuario.Id, usuario.Nombre, usuario.Sexo, usuario.TelefonoMovil, usuario.Email));
                return new CommandResult(true, "El usuario " + usuario.Nombre + " ha sido modificado correctamente.");
            }
            else
            {
                return new CommandResult(false, "There was a problem modifying this user: " + usuario.Nombre);
            }
        }
    }
}
