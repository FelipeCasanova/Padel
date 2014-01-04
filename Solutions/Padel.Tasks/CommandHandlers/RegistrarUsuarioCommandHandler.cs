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
    public class RegistrarUsuarioCommandHandler : ICommandHandler<RegistrarUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;
        private readonly IRepository<Role> roleRepository;

        public RegistrarUsuarioCommandHandler(IUsuarioTasks usuarioTasks, IRepository<Role> roleRepository)
        {
            this.usuarioTasks = usuarioTasks;
            this.roleRepository = roleRepository;
        }

        public CommandResult Handle(RegistrarUsuarioCommand command)
        {
            Usuario usuario = new Usuario();

            usuario.Nombre = command.Nombre;
            usuario.Email = command.Email;
            usuario.TelefonoMovil = command.TelefonoMovil;
            usuario.Sexo = command.Sexo;
            usuario.Password = MD5.Create().GetMd5Hash(command.Password);
            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaModificacion = DateTime.Now;
            usuario.Roles.Add(roleRepository.Get(2));

            if (usuario.IsValid())
            {
                this.usuarioTasks.CreateOrUpdate(usuario);
                return new CommandResult(true);
            }
            else 
            {
                return new CommandResult(false);
            }
        }
    }
    
}
