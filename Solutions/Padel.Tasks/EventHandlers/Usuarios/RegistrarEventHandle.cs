using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Padel.Domain.Events.Usuarios;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain.Operaciones;
using Padel.Domain;
using SharpArch.NHibernate.Contracts.Repositories;

namespace Padel.Tasks.EventHandlers.Usuarios
{
    public class RegistrarEventHandle : INotificationHandler<RegistrarEvent>
    {
        private readonly INHibernateRepository<Operacion> operacionRepository;
        private readonly INHibernateRepository<Usuario> usuarioRepository;

        public RegistrarEventHandle(INHibernateRepository<Operacion> operacionRepository, INHibernateRepository<Usuario> usuarioRepository)
        {
            this.operacionRepository = operacionRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public void Handle(RegistrarEvent args)
        {
            UsuarioOperacion operacion = new UsuarioOperacion();
            operacion.Accion = UsuarioOperacion.AccionEnum.Registrar.ToString();
            operacion.FechaCreacion = DateTime.Now;
            operacion.FechaModificacion = operacion.FechaCreacion;
            operacion.Mensaje = new StringBuilder("Garcias por su registro.").ToString();
            operacion.Usuario = this.usuarioRepository.Get(args.UsuarioId);
            this.operacionRepository.Save(operacion);
        }
    }
}
