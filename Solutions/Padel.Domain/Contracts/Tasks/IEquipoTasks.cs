using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Domain.Contracts.Tasks
{
    public interface IEquipoTasks
    {
        List<Equipo> GetAll();

        List<Equipo> GetEquiposPorJugadorList(int idJugador, params EstadoEquipoEnum[] estados);

        List<Equipo> GetEquiposPorJugadoresList(int idJugadorA, int idJugadorB, params EstadoEquipoEnum[] estados);

        Equipo Get(int id);

        Equipo CreateOrUpdate(Equipo equipo, int userId);

        Equipo CreateOrUpdate(Equipo equipo, int jugador1Id, int jugador2Id, int userId);

        void Delete(int id);
    }
}
