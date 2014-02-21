using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain.Contracts.Tasks;
using Padel.Domain.Operaciones;
using SharpArch.NHibernate;
using SharpArch.Domain.PersistenceSupport;

namespace Padel.Tasks
{
    public class OperacionTasks : NHibernateQuery, IOperacionTasks
    {
        private readonly IRepository<Operacion> operacionRepository;


        public OperacionTasks(IRepository<Operacion> operacionRepository)
        {
            this.operacionRepository = operacionRepository;
        }

        public List<Operacion> GetAllByUsuario(int usuarioId)
        {
            return Session.QueryOver<Operacion>().OrderBy(x => x.FechaCreacion).Desc.Where(o => o.Usuario.Id == usuarioId).Future().ToList();
        }

        public UsuarioOperacion GetOperacionEntrarByUsuario(int usuarioId)
        {
            return Session.QueryOver<UsuarioOperacion>().Where(o => o.Usuario.Id == usuarioId && o.Accion == UsuarioOperacion.AccionEnum.Entrar.ToString()).Future().FirstOrDefault();
        }

        public Operacion Get(int id)
        {
            return this.operacionRepository.Get(id);
        }

        public Operacion CreateOrUpdate(Operacion operacion)
        {
            return this.operacionRepository.SaveOrUpdate(operacion);
        }

        public void Delete(int id)
        {
            var operacion = this.operacionRepository.Get(id);
            this.operacionRepository.Delete(operacion);
        }
    }
}
