using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Padel.Domain.Events.Usuarios;
using Padel.Domain.Notificaciones;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain;
using System.ComponentModel.DataAnnotations;

namespace Padel.Tasks.EventHandlers.Usuarios
{
    public class IngresarCorazonesEventHandle : INotificationHandler<IngresarCorazonesEvent>
    {
        private readonly IRepository<Notificacion> notificacionRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public IngresarCorazonesEventHandle(IRepository<Notificacion> notificacionRepository, IRepository<Usuario> usuarioRepository)
        {
            this.notificacionRepository = notificacionRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public void Handle(IngresarCorazonesEvent args)
        {
            Usuario usuario = this.usuarioRepository.Get(args.UsuarioId);
            var validatorCtx = new ValidationContext(usuario);
            if (usuario.IsValid(validatorCtx))
            {
                usuario.AplicacionExperiencia += args.CantidadPuntos;
                this.usuarioRepository.SaveOrUpdate(usuario);
                CorazonNotificacion notificacion = new CorazonNotificacion();
                notificacion.Accion = args.Accion;
                notificacion.FechaCreacion = DateTime.Now;
                notificacion.FechaModificacion = notificacion.FechaCreacion;
                notificacion.Mensaje = new StringBuilder("Has conseguido ").Append(args.CantidadPuntos).Append(" corazones más!").ToString();
                notificacion.CantidadPuntos = args.CantidadPuntos;
                notificacion.Usuario = this.usuarioRepository.Get(args.UsuarioId);
                this.notificacionRepository.SaveOrUpdate(notificacion);
            }
        }
    }
}
