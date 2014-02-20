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
                DomainEvents.Raise<RegistrarEvent>(new RegistrarEvent(usuario.Id));
                return new CommandResult(true, string.Empty);
            }
            else 
            {
                return new CommandResult(false, string.Empty);
            }
        }
    }
    
}
