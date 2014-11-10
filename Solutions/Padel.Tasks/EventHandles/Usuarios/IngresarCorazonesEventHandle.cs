using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;
using Padel.Tasks.Events.Usuarios;
using Padel.Domain.Notificaciones;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain;

namespace Padel.Tasks.EventHandles.Usuarios
{
    public class IngresarCorazonesEventHandle : IHandles<IngresarCorazonesEvent>
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
            if (usuario.IsValid())
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
