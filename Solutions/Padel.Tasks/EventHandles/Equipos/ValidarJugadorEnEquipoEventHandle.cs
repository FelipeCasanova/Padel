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
    public class ValidarJugadorEnEquipoEventHandle : IHandles<ValidarJugadorEnEquipoEvent>
    {
        private readonly IRepository<Operacion> operacionRepository;
        private readonly IRepository<Usuario> usuarioRepository;
        private readonly IRepository<Equipo> equipoRepository;

        public ValidarJugadorEnEquipoEventHandle(IRepository<Operacion> operacionRepository, IRepository<Usuario> usuarioRepository, IRepository<Equipo> equipoRepository)
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
            this.operacionRepository.SaveOrUpdate(operacion);
        }
    }
}
