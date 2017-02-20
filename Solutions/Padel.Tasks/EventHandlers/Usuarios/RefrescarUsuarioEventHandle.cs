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
    public class RefrescarUsuarioEventHandle : INotificationHandler<RefrescarUsuarioEvent>
    {
        private readonly IUsuarioTasks usuarioTasks;

        public RefrescarUsuarioEventHandle(IUsuarioTasks usuarioTasks)
        {
            this.usuarioTasks = usuarioTasks;
        }

        public void Handle(RefrescarUsuarioEvent args)
        {
            usuarioTasks.RefreshUser();
        }
    }
}
