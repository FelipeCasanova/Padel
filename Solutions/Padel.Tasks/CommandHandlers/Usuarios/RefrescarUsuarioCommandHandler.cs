using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.IdentityModel.Web;
using Padel.Domain.Contracts.Tasks;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands.Usuarios;
using Padel.Domain;
using MediatR;

namespace Padel.Tasks.CommandHandlers.Usuarios
{
    public class RefrescarUsuarioCommandHandler : IRequestHandler<RefrescarUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;

        public RefrescarUsuarioCommandHandler(IUsuarioTasks usuarioTasks)
        {
            this.usuarioTasks = usuarioTasks;
        }

        public CommandResult Handle(RefrescarUsuarioCommand command)
        {
            usuarioTasks.RefreshUser(command.TelefonoMovil);
            return new CommandResult(true, string.Empty);
        }
    }
}
