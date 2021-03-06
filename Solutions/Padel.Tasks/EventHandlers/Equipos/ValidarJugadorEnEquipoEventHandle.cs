﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Padel.Domain;
using Padel.Domain.Operaciones;
using Padel.Infrastructure.Utilities;
using Padel.Domain.Events.Equipos;
using MediatR;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Contracts.Repositories;

namespace Padel.Tasks.EventHandlers.Equipos
{
    public class ValidarJugadorEnEquipoEventHandle : INotificationHandler<ValidarJugadorEnEquipoEvent>
    {
        private readonly INHibernateRepository<Operacion> operacionRepository;
        private readonly INHibernateRepository<Usuario> usuarioRepository;
        private readonly INHibernateRepository<Equipo> equipoRepository;

        public ValidarJugadorEnEquipoEventHandle(INHibernateRepository<Operacion> operacionRepository, INHibernateRepository<Usuario> usuarioRepository, INHibernateRepository<Equipo> equipoRepository)
        {
            this.operacionRepository = operacionRepository;
            this.usuarioRepository = usuarioRepository;
            this.equipoRepository = equipoRepository;
        }

        public void Handle(ValidarJugadorEnEquipoEvent args)
        {
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;

            EquipoOperacion operacion = new EquipoOperacion();
            operacion.Accion = EquipoOperacion.AccionEnum.ValidarJugadorEnEquipo.ToString();
            operacion.FechaCreacion = DateTime.Now;
            operacion.FechaModificacion = operacion.FechaCreacion;
            operacion.Equipo = this.equipoRepository.Get(args.EquipoId);

            if (principal.Id == args.JugadorId)
            {
                
                operacion.Mensaje = new StringBuilder("Te has validado en el equipo ")
                    .Append(operacion.Equipo.ToString()).Append(".")
                    .ToString();
            }
            else
            {
                var jugador = this.usuarioRepository.Get(args.JugadorId);
                
                operacion.Mensaje = new StringBuilder("Has validado al jugador ")
                    .Append(jugador.Nombre).Append(" en el equipo ")
                    .Append(operacion.Equipo.ToString()).Append(".")
                    .ToString();
            }
            
            operacion.Usuario = this.usuarioRepository.Get(principal.Id);
            this.operacionRepository.Save(operacion);
        }
    }
}
