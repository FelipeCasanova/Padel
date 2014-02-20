using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Padel.Tasks
{
    public class UsuarioTasks : NHibernateQuery, IUsuarioTasks
    {
        private readonly IRepository<Usuario> usuarioRepository;

        public UsuarioTasks(IRepository<Usuario> usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public List<Domain.Usuario> GetAll()
        {
            var allUsuarios = this.usuarioRepository.GetAll().ToList();
            return allUsuarios;
        }

        public Domain.Usuario Get(int id)
        {
            return this.usuarioRepository.Get(id);
        }

        public Domain.Usuario CreateOrUpdate(Domain.Usuario usuario)
        {
            this.usuarioRepository.SaveOrUpdate(usuario);
            return usuario;
        }

        public void Delete(int id)
        {
            var usuario = this.usuarioRepository.Get(id);
            this.usuarioRepository.Delete(usuario);
        }

        public Domain.Usuario GetByEmail(string email)
        {
            var query = Session.QueryOver<Usuario>();
            return query.Where(u => u.Email == email).SingleOrDefault();
        }

        public Domain.Usuario GetByMovil(int telefonoMovil)
        {
            var query = Session.QueryOver<Usuario>();
            return query.Where(u => u.TelefonoMovil == telefonoMovil).SingleOrDefault();
        }


        public int GetNumeroUsuariosByMovil(int telefonoMovil)
        {
            var query = Session.QueryOver<Usuario>();
            return query.Where(u => u.TelefonoMovil == telefonoMovil).RowCount();
        }

        public int GetNumeroUsuariosByEmail(string email)
        {
            var query = Session.QueryOver<Usuario>();
            return query.Where(u => u.Email == email).RowCount();
        }


        public bool ValidateUser(string emailOrMovil, string password, out int telefonoMovilOut)
        {
            telefonoMovilOut = 0;
            int telefonoMovil = 0;
            bool isNumeroTelefonico = Int32.TryParse(emailOrMovil, out telefonoMovil);
            var query = Session.QueryOver<Usuario>();
            if (isNumeroTelefonico)
            {
                Usuario usuario = query.Where(u => u.TelefonoMovil == telefonoMovil && u.Password == password).SingleOrDefault();
                if (usuario != null)
                {
                    telefonoMovilOut = usuario.TelefonoMovil;
                    return true;
                }
            }
            else
            {
                Usuario usuario = query.Where(u => u.Email == emailOrMovil && u.Password == password).SingleOrDefault();
                if (usuario != null)
                {
                    telefonoMovilOut = usuario.TelefonoMovil;
                    return true;
                }
            }
            return false;
        }
    }
}
