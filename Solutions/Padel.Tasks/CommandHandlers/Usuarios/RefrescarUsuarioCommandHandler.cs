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
using SharpArch.Domain.Commands;

namespace Padel.Tasks.CommandHandlers.Usuarios
{
    public class RefrescarUsuarioCommandHandler : ICommandHandler<RefrescarUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;

        public RefrescarUsuarioCommandHandler(IUsuarioTasks usuarioTasks)
        {
            this.usuarioTasks = usuarioTasks;
        }

        public CommandResult Handle(RefrescarUsuarioCommand command)
        {
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            var usuarioDB = usuarioTasks.Get(principal.Id);
            var token = FederatedAuthentication.SessionAuthenticationModule.CreateSessionSecurityToken(ClaimsPrincipalUtility.CreatePrincipal(usuarioDB),
                "PadelContext", DateTime.Now, DateTime.Now.AddHours(1), true);
            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(token);
            return new CommandResult(true, string.Empty);
        }
    }
}
