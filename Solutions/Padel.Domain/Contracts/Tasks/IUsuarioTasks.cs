using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Domain.Contracts.Tasks
{
    public interface IUsuarioTasks
    {
        List<Usuario> GetAll();

        Usuario Get(int id);

        Usuario CreateOrUpdate(Usuario productModel);

        void Delete(int id);

        bool ValidateUser(string emailOrMovil, string password, out int telefonoMovilOut);

        Usuario GetByMovil(int telefonoMovil);

        int GetNumeroUsuariosByMovil(int telefonoMovil);

        int GetNumeroUsuariosByEmail(string email);
    }
}
