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
using SharpArch.Domain.PersistenceSupport;
using MediatR;
using Padel.Domain.Events.Usuarios;

namespace Padel.Tasks.CommandHandlers.Usuarios
{
    public class EntrarUsuarioCommandHandler : IRequestHandler<EntrarUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;
        private readonly IMediator mediator;

        public EntrarUsuarioCommandHandler(IUsuarioTasks usuarioTasks, IMediator mediator)
        {
            this.usuarioTasks = usuarioTasks;
            this.mediator = mediator;
        }

        public CommandResult Handle(EntrarUsuarioCommand command)
        {
            var password = MD5.Create().GetMd5Hash(command.Password);
            var result = usuarioTasks.ValidateUser(command.EmailOrMovil, password, out command.TelefonoMovil);
            if (result)
            {
                mediator.Publish(new EntrarEvent(command.EmailOrMovil));
            }
            return new CommandResult(result, string.Empty);
        }
    }

}
