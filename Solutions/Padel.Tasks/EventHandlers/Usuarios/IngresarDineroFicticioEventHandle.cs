using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Padel.Domain.Events.Usuarios;
using Padel.Domain.Notificaciones;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain;

namespace Padel.Tasks.EventHandlers.Usuarios
{
    public class IngresarDineroFicticioEventHandle : INotificationHandler<IngresarDineroFicticioEvent>
    {
        private readonly IRepository<Notificacion> notificacionRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public IngresarDineroFicticioEventHandle(IRepository<Notificacion> notificacionRepository, IRepository<Usuario> usuarioRepository)
        {
            this.notificacionRepository = notificacionRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public void Handle(IngresarDineroFicticioEvent args)
        {
            DineroNotificacion notificacion = new DineroNotificacion();
            notificacion.Accion = DineroNotificacion.AccionEnum.IngresoPuntosFicticios.ToString();
            notificacion.FechaCreacion = DateTime.Now;
            notificacion.FechaModificacion = notificacion.FechaCreacion;
            notificacion.Mensaje = new StringBuilder("Ingreso de ").Append(args.CantidadPuntos).Append(" puntos.").ToString();
            notificacion.CantidadPuntos = args.CantidadPuntos;
            notificacion.Usuario = this.usuarioRepository.Get(args.UsuarioId);
            this.notificacionRepository.SaveOrUpdate(notificacion);
        }
    }
}
