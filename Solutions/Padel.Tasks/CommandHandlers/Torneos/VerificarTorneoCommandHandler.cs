using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Tasks.Commands.Torneos;
using Padel.Tasks.CommandResults;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain;
using MediatR;
using SharpArch.NHibernate.Contracts.Repositories;

namespace Padel.Tasks.CommandHandlers.Torneos
{
    public class VerificarTorneoCommandHandler : IRequestHandler<VerificarTorneoCommand, CommandResult>
    {
        private readonly INHibernateRepository<Torneo> torneoRepository;
        private readonly INHibernateRepository<Categoria> categoriaRepository;

        public VerificarTorneoCommandHandler(INHibernateRepository<Torneo> torneoRepository, INHibernateRepository<Categoria> categoriaRepository)
        {
            this.torneoRepository = torneoRepository;
            this.categoriaRepository = categoriaRepository;
        }

        public CommandResult Handle(VerificarTorneoCommand command)
        {
            Categoria categoria = this.categoriaRepository.Get(command.CategoriaId);

            var equiposPendientePorPagar = categoria.EquiposToCategorias.Where(ec => ec.Estado == EstadoEquipoCategoriaEnum.Pendiente);
            if (equiposPendientePorPagar.Any())
            {
                categoria.Estado = EstadoCategoriaEnum.Pendiente;
                categoria.FechaModificacion = DateTime.Now;
                this.categoriaRepository.Update(categoria);
                return new CommandResult(false, "Hay equipos que no han pagado aun.", equiposPendientePorPagar.ToList());
            }
            else
            {
                // Verificar estado
                categoria.Estado = EstadoCategoriaEnum.Verificado;
                categoria.FechaModificacion = DateTime.Now;
                this.categoriaRepository.Update(categoria);
                return new CommandResult(true, "El torneo ha sido verificado.");
            }
        }
    }
}
