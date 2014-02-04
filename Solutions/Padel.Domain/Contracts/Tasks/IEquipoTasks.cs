using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Domain.Contracts.Tasks
{
    public interface IEquipoTasks
    {
        List<Equipo> GetAll();

        List<Equipo> GetEquiposPorJugadoresList(int idJugadorA, int idJugadorB, EstadoEquipoEnum estado);

        Equipo Get(int id);

        Equipo CreateOrUpdate(Equipo productModel);

        void Delete(int id);
    }
}
