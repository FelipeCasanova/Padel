using System;
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
    public class EliminarEquipoEventHandle : INotificationHandler<EliminarEquipoEvent>
    {
        private readonly INHibernateRepository<Operacion> operacionRepository;
        private readonly INHibernateRepository<Usuario> usuarioRepository;
        private readonly INHibernateRepository<Equipo> equipoRepository;

        public EliminarEquipoEventHandle(INHibernateRepository<Operacion> operacionRepository, INHibernateRepository<Usuario> usuarioRepository, INHibernateRepository<Equipo> equipoRepository)
        {
            this.operacionRepository = operacionRepository;
            this.usuarioRepository = usuarioRepository;
            this.equipoRepository = equipoRepository;
        }

        public void Handle(EliminarEquipoEvent args)
        {
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;

            EquipoOperacion operacion = new EquipoOperacion();
            operacion.Accion = EquipoOperacion.AccionEnum.EliminarEquipo.ToString();
            operacion.FechaCreacion = DateTime.Now;
            operacion.FechaModificacion = operacion.FechaCreacion;


            if (principal.Id == args.Jugador1Id)
            {
                var jugador = this.usuarioRepository.Get(args.Jugador2Id);
                operacion.Mensaje = new StringBuilder("Has eliminado tú equipo con ")
                    .Append(jugador.Nombre).Append(".")
                    .ToString();
            }
            else
            {
                var jugador1 = this.usuarioRepository.Get(args.Jugador1Id);
                var jugador2 = this.usuarioRepository.Get(args.Jugador2Id);
                operacion.Mensaje = new StringBuilder("Has eliminado el equipo de ")
                    .Append(jugador1.Nombre).Append(" y ")
                    .Append(jugador2.Nombre).Append(".")
                    .ToString();
            }

            operacion.Usuario = this.usuarioRepository.Get(principal.Id);
            operacion.Equipo = this.equipoRepository.Get(args.EquipoId);
            this.operacionRepository.Save(operacion);
        }
    }
}
