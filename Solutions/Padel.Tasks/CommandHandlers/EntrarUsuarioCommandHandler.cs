using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using Padel.Tasks.Commands;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Web;
using Padel.Infrastructure.Utilities;
using Padel.Domain.Contracts.Tasks;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.CommandHandlers
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
            return new CommandResult(usuarioTasks.ValidateUser(command.EmailOrMovil, password, out command.TelefonoMovil), string.Empty);
        }
    }
    
}
