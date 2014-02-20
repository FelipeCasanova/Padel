﻿using System;
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
        private readonly IRepository<Usuario> usuarioRepository;

        public EquipoTasks(IRepository<Equipo> equipoRepository, IRepository<Usuario> usuarioRepository)
        {
            this.equipoRepository = equipoRepository;
            this.usuarioRepository = usuarioRepository;
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

        public Equipo CreateOrUpdate(Equipo equipo, int userId)
        {
            return CreateOrUpdate(equipo, 0, 0, userId);
        }

        public Equipo CreateOrUpdate(Equipo equipo, int jugador1Id, int jugador2Id, int userId)
        {
            if (equipo.Id != 0)
            {
                equipo.FechaModificacion = DateTime.Now;
            }
            else
            {
                equipo.FechaCreacion = DateTime.Now;
                equipo.FechaModificacion = equipo.FechaCreacion;
                equipo.Nombre = null;
                equipo.JugadorA = this.usuarioRepository.Get(jugador1Id);
                equipo.JugadorB = this.usuarioRepository.Get(jugador2Id);

                if (equipo.JugadorA.Sexo == SexoEnum.Hombre && equipo.JugadorB.Sexo == SexoEnum.Hombre)
                {
                    equipo.TipoEquipo = TipoEquipoEnum.Hombre;
                }
                else if (equipo.JugadorA.Sexo == SexoEnum.Mujer && equipo.JugadorB.Sexo == SexoEnum.Mujer)
                {
                    equipo.TipoEquipo = TipoEquipoEnum.Mujer;
                }
                else
                {
                    equipo.TipoEquipo = TipoEquipoEnum.Mixto;
                }

                equipo.JugadorAVerificado = false;
                equipo.JugadorBVerificado = false;
            }

            // Activar usuario y equipo si se diera el caso
            if (equipo.JugadorA.Id == userId && !equipo.JugadorAVerificado)
            {
                equipo.JugadorAVerificado = true;
            }

            if (equipo.JugadorB.Id == userId && !equipo.JugadorBVerificado)
            {
                equipo.JugadorBVerificado = true;
            }

            if (equipo.JugadorAVerificado && equipo.JugadorBVerificado)
            {
                equipo.Estado = EstadoEquipoEnum.Activado;
            }
            else
            {
                equipo.Estado = EstadoEquipoEnum.Desactivado;
            }

            this.CreateOrUpdate(equipo);
            return equipo;
        }

        protected Equipo CreateOrUpdate(Equipo equipo)
        {
            if (equipo.IsValid())
            {
                this.equipoRepository.SaveOrUpdate(equipo);
            }
            return equipo;
        }

        public void Delete(int id)
        {
            var equipo = this.equipoRepository.Get(id);
            this.equipoRepository.Delete(equipo);
        }

        public List<Equipo> GetEquiposPorJugadorList(int idJugador, params EstadoEquipoEnum[] estados)
        {
            var query = Session.QueryOver<Equipo>().OrderBy(x => x.Nombre).Asc;
            query = query.Where(e => e.JugadorA.Id == idJugador);

            foreach (var estado in estados)
            {
                query = query.Where(e => (e.Estado == EstadoEquipoEnum.Activado));
            }
            return query.Future().ToList();
        }

        public List<Equipo> GetEquiposPorJugadoresList(int idJugador1, int idJugador2, params EstadoEquipoEnum[] estados)
        {
            var query = Session.QueryOver<Equipo>().OrderBy(x => x.Nombre).Asc;
            query = query.Where(e => (e.JugadorA.Id == idJugador1 || e.JugadorB.Id == idJugador1)
                && (e.JugadorA.Id == idJugador2 || e.JugadorB.Id == idJugador2));

            foreach (var estado in estados)
            {
                query = query.Where(e => (e.Estado == EstadoEquipoEnum.Activado));
            }
            return query.Future().ToList();
        }

    }
}