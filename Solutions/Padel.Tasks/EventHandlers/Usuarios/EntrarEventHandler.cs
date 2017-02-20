using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Padel.Domain.Events.Usuarios;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain.Operaciones;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;

namespace Padel.Tasks.EventHandlers.Usuarios
{
    public class EntrarEventHandler : INotificationHandler<EntrarEvent>
    {
        private readonly IOperacionTasks operacionTasks;
        private readonly IUsuarioTasks usuarioTasks;

        public EntrarEventHandler(IOperacionTasks operacionTasks, IUsuarioTasks usuarioTasks)
        {
            this.operacionTasks = operacionTasks;
            this.usuarioTasks = usuarioTasks;
        }

        public void Handle(EntrarEvent args)
        {
            Usuario usuario = null;
            int telefonoMovil = 0;
            bool isNumeroTelefonico = Int32.TryParse(args.EmailOrMovil, out telefonoMovil);
            if (isNumeroTelefonico)
            {
                usuario = this.usuarioTasks.GetByMovil(telefonoMovil);
            }
            else
            {
                usuario = this.usuarioTasks.GetByEmail(args.EmailOrMovil);
            }
            UsuarioOperacion operacion = this.operacionTasks.GetOperacionEntrarByUsuario(usuario.Id) ?? new UsuarioOperacion();
            operacion.Usuario = usuario;
            operacion.Accion = UsuarioOperacion.AccionEnum.Entrar.ToString();
            if (operacion.Id == 0)
            {
                operacion.FechaCreacion = DateTime.Now;
                operacion.FechaModificacion = operacion.FechaCreacion;
            }
            else
            {
                operacion.FechaCreacion = operacion.FechaModificacion;
                operacion.FechaModificacion = DateTime.Now;
            }
            operacion.Mensaje = new StringBuilder("Has entrado por última vez el ")
                .Append(operacion.FechaCreacion.ToLongDateString())
                .Append(" a las ")
                .Append(operacion.FechaCreacion.ToLongTimeString())
                .Append(".").ToString();
            this.operacionTasks.CreateOrUpdate(operacion);
        }
    }
}
