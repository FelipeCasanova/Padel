using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;
using Padel.Tasks.Events;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain.Operaciones;
using Padel.Domain;

namespace Padel.Tasks.EventHandles
{
    public class RegistroEventHandle : IHandles<RegistroEvent>
    {
        private readonly IRepository<Operacion> operacionRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public RegistroEventHandle(IRepository<Operacion> operacionRepository, IRepository<Usuario> usuarioRepository)
        {
            this.operacionRepository = operacionRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public void Handle(RegistroEvent args)
        {
            UsuarioOperacion operacion = new UsuarioOperacion();
            operacion.Accion = UsuarioOperacion.AccionEnum.Registrar.ToString();
            operacion.FechaCreacion = DateTime.Now;
            operacion.FechaModificacion = operacion.FechaCreacion;
            operacion.Mensaje = new StringBuilder("Garcias por su registro con fecha: ").Append(DateTime.Now.ToLongDateString()).ToString();
            operacion.Usuario = this.usuarioRepository.Get(args.UsuarioId);
            this.operacionRepository.SaveOrUpdate(operacion);
        }
    }
}
