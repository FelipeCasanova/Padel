using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain.Operaciones;

namespace Padel.Domain.Contracts.Tasks
{
    public interface IOperacionTasks
    {
        UsuarioOperacion GetOperacionEntrarByUsuario(int usuarioId);

        List<Operacion> GetAllByUsuario(int usuarioId);

        Operacion Get(int id);

        Operacion CreateOrUpdate(Operacion operacion);

        void Delete(int id);
    }
}
