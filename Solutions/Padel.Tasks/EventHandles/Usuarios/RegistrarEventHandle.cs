using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;
using Padel.Tasks.Events.Usuarios;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain.Operaciones;
using Padel.Domain;

namespace Padel.Tasks.EventHandles.Usuarios
{
    public class RegistrarEventHandle : IHandles<RegistrarEvent>
    {
        private readonly IRepository<Operacion> operacionRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public RegistrarEventHandle(IRepository<Operacion> operacionRepository, IRepository<Usuario> usuarioRepository)
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
            this.operacionRepository.SaveOrUpdate(operacion);
        }
    }
}
