using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;
using Padel.Tasks.Events.Usuarios;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain.Operaciones;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;

namespace Padel.Tasks.EventHandles.Usuarios
{
    public class RefrescarUsuarioEventHandle : IHandles<RefrescarUsuarioEvent>
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
