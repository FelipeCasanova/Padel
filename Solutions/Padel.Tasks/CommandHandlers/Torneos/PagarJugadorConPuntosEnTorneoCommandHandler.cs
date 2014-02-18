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
    public class PagarJugadorConPuntosEnTorneoCommandHandler : ICommandHandler<PagarJugadorConPuntosEnTorneoCommand, CommandResult>
    {
        private readonly IRepository<EquipoToCategoria> equipoToCategoriaRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public PagarJugadorConPuntosEnTorneoCommandHandler(IRepository<EquipoToCategoria> equipoToCategoriaRepository, IRepository<Usuario> usuarioRepository)
        {
            this.equipoToCategoriaRepository = equipoToCategoriaRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public CommandResult Handle(PagarJugadorConPuntosEnTorneoCommand command)
        {
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            Usuario usuario = this.usuarioRepository.Get(principal.Id);
            EquipoToCategoria equiposToCategoria = this.equipoToCategoriaRepository.Get(command.EquipoCategoriaId);

            // Tipo debe ser 1: se paga un jugador | 2: Pagamos todo
            if (command.Tipo != 1 && command.Tipo != 2)
            {
                this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se ha podido pagar el torneo.");
            }

            // No hay puntos suficientes para pagar
            if (equiposToCategoria.Categoria.Precio * (command.Tipo == 1 ? 1 : 2) > usuario.DineroFicticio)
            {
                this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se ha podido pagar el torneo porque no tienes suficientes puntos.");
            }

            // El torneo ya ha sido pagado
            if (equiposToCategoria.Categoria.Precio * 2 <= (equiposToCategoria.DineroFicticioJugadorA + equiposToCategoria.DineroRealJugadorA +
                                                            equiposToCategoria.DineroFicticioJugadorB + equiposToCategoria.DineroRealJugadorB))
            {   
                this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "El torneo ya ha sido pagado.");
            }

            // Jugador 1
            if (equiposToCategoria.Equipo.JugadorA.Id == usuario.Id)
            {
                equiposToCategoria.DineroFicticioJugadorA += equiposToCategoria.Categoria.Precio * (command.Tipo == 1 ? 1 : 2);
                usuario.DineroFicticio -= equiposToCategoria.Categoria.Precio * (command.Tipo == 1 ? 1 : 2);
            }

            // Jugador 2
            if (equiposToCategoria.Equipo.JugadorB.Id == usuario.Id)
            {
                equiposToCategoria.DineroFicticioJugadorB += equiposToCategoria.Categoria.Precio * (command.Tipo == 1 ? 1 : 2);
                usuario.DineroFicticio -= equiposToCategoria.Categoria.Precio * (command.Tipo == 1 ? 1 : 2);
            }

            // Cambiamos estado si ya esta pagado
            if ((equiposToCategoria.DineroFicticioJugadorA + equiposToCategoria.DineroRealJugadorA +
                equiposToCategoria.DineroFicticioJugadorB + equiposToCategoria.DineroRealJugadorB) == equiposToCategoria.Categoria.Precio * 2)
            {
                equiposToCategoria.Estado = EstadoEquipoCategoriaEnum.Pagado;
            }

            if (equiposToCategoria.IsValid())
            {
                this.equipoToCategoriaRepository.SaveOrUpdate(equiposToCategoria);
                return new CommandResult(true, (equiposToCategoria.Estado != EstadoEquipoCategoriaEnum.Pagado ? "Se ha pagado correctamente uno de los miembros del equipo." : "Se ha pagado correctamente el torneo."));
            }
            else
            {
                this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se ha podido pagar el torneo. Intentelo más tarde.");
            }

        }
    }
}
