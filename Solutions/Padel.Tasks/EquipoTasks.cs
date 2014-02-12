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

        public Equipo CreateOrUpdate(Equipo equipo, int jugador1Id, int jugador2Id)
        {
            if (equipo.Id != 0)
            {
                // Activar jugador y equipo si se diera el caso
                if (equipo.JugadorA.Id == jugador1Id && !equipo.JugadorAVerificado)
                {
                    equipo.JugadorAVerificado = true;
                }

                if (equipo.JugadorB.Id == jugador1Id && !equipo.JugadorBVerificado)
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

                equipo.FechaModificacion = DateTime.Now;
            }
            else
            {
                equipo.FechaCreacion = DateTime.Now;
                equipo.FechaModificacion = equipo.FechaCreacion;
                equipo.Estado = EstadoEquipoEnum.Desactivado;
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

                equipo.JugadorAVerificado = true;
                equipo.JugadorBVerificado = false;
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
            var usuario = this.equipoRepository.Get(id);
            this.equipoRepository.Delete(usuario);
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

        public List<Equipo> GetEquiposPorJugadoresList(int idJugadorA, int idJugadorB, params EstadoEquipoEnum[] estados)
        {
            var query = Session.QueryOver<Equipo>().OrderBy(x => x.Nombre).Asc;
            query = query.Where(e => (e.JugadorA.Id == idJugadorA || e.JugadorB.Id == idJugadorA)
                && (e.JugadorA.Id == idJugadorB || e.JugadorB.Id == idJugadorB));

            foreach (var estado in estados)
            {
                query = query.Where(e => (e.Estado == EstadoEquipoEnum.Activado));
            }
            return query.Future().ToList();
        }

    }
}
