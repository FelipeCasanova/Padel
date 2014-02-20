using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;
using Padel.Tasks.Events.Equipos;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain.Operaciones;
using Padel.Domain;

namespace Padel.Tasks.EventHandles.Equipos
{
    public class CrearEquipoEventHandle : IHandles<CrearEquipoEvent>
    {
        private readonly IRepository<Operacion> equipoRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public CrearEquipoEventHandle(IRepository<Operacion> equipoRepository, IRepository<Usuario> usuarioRepository)
        {
            this.equipoRepository = equipoRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public void Handle(CrearEquipoEvent args)
        {
            EquipoOperacion operacion = new EquipoOperacion();
            operacion.Accion = EquipoOperacion.AccionEnum.CrearEquipo.ToString();
            operacion.FechaCreacion = DateTime.Now;
            operacion.FechaModificacion = operacion.FechaCreacion;

            var jugador = this.usuarioRepository.Get(args.Jugador2Id);
            operacion.Mensaje = new StringBuilder("Has creado un nuevo equipo con ")
                .Append(jugador.Nombre).Append(".")
                .ToString();
            
            operacion.Usuario = this.usuarioRepository.Get(args.UsuarioId);
            this.equipoRepository.SaveOrUpdate(operacion);
        }
    }
}
