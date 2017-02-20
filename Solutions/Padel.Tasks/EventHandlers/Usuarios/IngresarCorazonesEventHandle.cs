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
using SharpArch.NHibernate.Contracts.Repositories;

namespace Padel.Tasks.EventHandlers.Usuarios
{
    public class IngresarCorazonesEventHandle : INotificationHandler<IngresarCorazonesEvent>
    {
        private readonly INHibernateRepository<Notificacion> notificacionRepository;
        private readonly INHibernateRepository<Usuario> usuarioRepository;

        public IngresarCorazonesEventHandle(INHibernateRepository<Notificacion> notificacionRepository, INHibernateRepository<Usuario> usuarioRepository)
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
                this.usuarioRepository.Update(usuario);
                CorazonNotificacion notificacion = new CorazonNotificacion();
                notificacion.Accion = args.Accion;
                notificacion.FechaCreacion = DateTime.Now;
                notificacion.FechaModificacion = notificacion.FechaCreacion;
                notificacion.Mensaje = new StringBuilder("Has conseguido ").Append(args.CantidadPuntos).Append(" corazones más!").ToString();
                notificacion.CantidadPuntos = args.CantidadPuntos;
                notificacion.Usuario = this.usuarioRepository.Get(args.UsuarioId);
                this.notificacionRepository.Save(notificacion);
            }
        }
    }
}
