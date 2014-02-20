using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Web;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Tasks.Commands.Usuarios;
using Padel.Tasks.Events.Usuarios;
using SharpArch.Domain.Commands;
using SharpArch.Domain.Events;
using SharpArch.Domain.PersistenceSupport;

namespace Padel.Tasks.CommandHandlers.Usuarios
{
    public class EntrarUsuarioCommandHandler : ICommandHandler<EntrarUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;

        public EntrarUsuarioCommandHandler(IUsuarioTasks usuarioTasks)
        {
            this.usuarioTasks = usuarioTasks;
        }

        public CommandResult Handle(EntrarUsuarioCommand command)
        {
            var password = MD5.Create().GetMd5Hash(command.Password);
            var result = usuarioTasks.ValidateUser(command.EmailOrMovil, password, out command.TelefonoMovil);
            if (result)
            {
                DomainEvents.Raise<EntrarEvent>(new EntrarEvent(command.EmailOrMovil));
            }
            return new CommandResult(result, string.Empty);
        }
    }

}
