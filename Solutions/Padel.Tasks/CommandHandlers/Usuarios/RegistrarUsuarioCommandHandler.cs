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
using Padel.Domain.Notificaciones;
using MediatR;
using Padel.Domain.Events.Usuarios;
using System.ComponentModel.DataAnnotations;

namespace Padel.Tasks.CommandHandlers.Usuarios
{
    public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioTasks usuarioTasks;
        private readonly IRepository<Role> roleRepository;
        private readonly IMediator mediator;

        public RegistrarUsuarioCommandHandler(IUsuarioTasks usuarioTasks, IRepository<Role> roleRepository, IMediator mediator)
        {
            this.usuarioTasks = usuarioTasks;
            this.roleRepository = roleRepository;
            this.mediator = mediator;
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
            usuario.Ip = command.IP;
            usuario.Roles.Add(roleRepository.Get(2));

            var validatorCtx = new ValidationContext(usuario);
            if (usuario.IsValid(validatorCtx))
            {
                this.usuarioTasks.CreateOrUpdate(usuario);
                mediator.Publish(new RegistrarEvent(usuario.Id));
                mediator.Publish(new IngresarCorazonesEvent(usuario.Id, 3, CorazonNotificacion.AccionEnum.IngresoCorazonesRegistro.ToString()));
                return new CommandResult(true, string.Empty);
            }
            else 
            {
                return new CommandResult(false, string.Empty);
            }
        }
    }
    
}
