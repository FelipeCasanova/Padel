using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Padel.Domain;
using Padel.Domain.Operaciones;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.Events.Equipos;
using SharpArch.Domain.Events;
using SharpArch.Domain.PersistenceSupport;

namespace Padel.Tasks.EventHandles.Equipos
{
    public class EliminarEquipoEventHandle : IHandles<EliminarEquipoEvent>
    {
        private readonly IRepository<Operacion> operacionRepository;
        private readonly IRepository<Usuario> usuarioRepository;
        private readonly IRepository<Equipo> equipoRepository;

        public EliminarEquipoEventHandle(IRepository<Operacion> operacionRepository, IRepository<Usuario> usuarioRepository, IRepository<Equipo> equipoRepository)
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
            this.operacionRepository.SaveOrUpdate(operacion);
        }
    }
}
