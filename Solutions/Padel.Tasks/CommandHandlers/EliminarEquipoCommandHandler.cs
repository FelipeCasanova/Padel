using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;
using Padel.Tasks.Commands;
using Padel.Tasks.CommandResults;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain;
using System.Threading;
using Padel.Infrastructure.Utilities;

namespace Padel.Tasks.CommandHandlers
{
    public class EliminarEquipoCommandHandler : ICommandHandler<EliminarEquipoCommand, CommandResult>
    {
        private readonly IRepository<Equipo> equipoRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public EliminarEquipoCommandHandler(IRepository<Usuario> usuarioRepository, IRepository<Equipo> equipoRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.equipoRepository = equipoRepository;
        }

        public CommandResult Handle(EliminarEquipoCommand command)
        {
            Equipo equipo = this.equipoRepository.Get(command.IdEquipo);
            if (equipo.EquiposToCategorias.Any(ec => ec.Categoria.Estado == EstadoCategoriaEnum.Pendiente || ec.Categoria.Estado == EstadoCategoriaEnum.Progreso))
            {
                return new CommandResult(false, "No se puede eliminar el equipo porque esta apuntado a un torneo.");
            }

            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            if (equipo.JugadorA.Id == command.IdJugador || equipo.JugadorB.Id == command.IdJugador || principal.IsInRole("Administrador"))
            {   
                equipo.JugadorAVerificado = false;
                equipo.JugadorBVerificado = false;
                equipo.Estado = EstadoEquipoEnum.Eliminado;
            }
            
            this.equipoRepository.SaveOrUpdate(equipo);
            return new CommandResult(true, "Se ha eliminado correctamente el equipo.");
        }
    }
}
