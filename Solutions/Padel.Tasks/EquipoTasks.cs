using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain.Contracts.Tasks;
using Padel.Domain;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Padel.Tasks
{
    public class EquipoTasks : NHibernateQuery, IEquipoTasks
    {
        private readonly IRepository<Equipo> equipoRepository;

        public EquipoTasks(IRepository<Equipo> equipoRepository)
        {
            this.equipoRepository = equipoRepository;
        }

        public List<Equipo> GetAll()
        {
            var allEquipos = this.equipoRepository.GetAll().ToList();
            return allEquipos;
        }

        public Equipo Get(int id)
        {
            return this.equipoRepository.Get(id);
        }

        public Equipo CreateOrUpdate(Equipo equipo)
        {
            this.equipoRepository.SaveOrUpdate(equipo);
            return equipo;
        }

        public void Delete(int id)
        {
            var usuario = this.equipoRepository.Get(id);
            this.equipoRepository.Delete(usuario);
        }


        public List<Equipo> GetEquiposPorJugadoresList(int idJugadorA, int idJugadorB, EstadoEquipoEnum estado)
        {
            var query = Session.QueryOver<Equipo>().OrderBy(x => x.Nombre).Asc;
            return query.Where(e => (e.JugadorA.Id == idJugadorA || e.JugadorB.Id == idJugadorA)
                && (e.JugadorA.Id == idJugadorB || e.JugadorB.Id == idJugadorB)
                && e.Estado == EstadoEquipoEnum.Activado).Future().ToList();
        }

    }
}
