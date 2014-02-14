using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Padel.Domain;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands.Torneos;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;

namespace Padel.Tasks.CommandHandlers.Equipos
{
    public class EliminarEquipoDeTorneoCommandHandler : ICommandHandler<EliminarEquipoDeTorneoCommand, CommandResult>
    {
        private readonly IRepository<Equipo> equipoRepository;
        private readonly IRepository<EquipoToCategoria> equipoToCategoriaRepository;

        public EliminarEquipoDeTorneoCommandHandler(IRepository<EquipoToCategoria> equipoToCategoriaRepository, IRepository<Equipo> equipoRepository)
        {
            this.equipoToCategoriaRepository = equipoToCategoriaRepository;
            this.equipoRepository = equipoRepository;
        }

        public CommandResult Handle(EliminarEquipoDeTorneoCommand command)
        {
            Equipo equipo = this.equipoRepository.Get(command.EquipoId);
            EquipoToCategoria equiposToCategoria = equipo.EquiposToCategorias
                .Where(ec => ec.Categoria.Id == command.CategoriaId && ec.Equipo.Id == command.EquipoId && ec.Estado != EstadoEquipoCategoriaEnum.Eliminado)
                .FirstOrDefault();

            if (equiposToCategoria == null)
            {
                return new CommandResult(false, "No se ha podido eliminar el equipo del torneo. Intentelo más tarde.");
            }

            if (equiposToCategoria.Categoria.Estado == EstadoCategoriaEnum.Progreso)
            {
                return new CommandResult(false, "No se puede eliminar el equipo del torneo porque esta en progreso.");
            }

            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            if (equipo.JugadorA.Id == principal.Id || equipo.JugadorB.Id == principal.Id || principal.IsInRole("Administrador"))
            {
                //equiposToCategoria.Equipo.EquiposToCategorias.Remove(equiposToCategoria);
                //equiposToCategoria.Categoria.EquiposToCategorias.Remove(equiposToCategoria);
                //this.equipoToCategoriaRepository.Delete(equiposToCategoria);
                equiposToCategoria.Estado = EstadoEquipoCategoriaEnum.Eliminado;
                this.equipoToCategoriaRepository.SaveOrUpdate(equiposToCategoria);
                return new CommandResult(true, "Se ha eliminado correctamente el equipo del torneo.");    
            }
            else
            {
                return new CommandResult(false, "No se ha podido eliminar el equipo del torneo. Intentelo más tarde.");
            }
            
        }
    }
}
