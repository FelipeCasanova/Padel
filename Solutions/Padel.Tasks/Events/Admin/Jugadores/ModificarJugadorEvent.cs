using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Events;
using Padel.Domain;

namespace Padel.Tasks.Events.Admin.Jugadores
{
    public class ModificarJugadorEvent : IDomainEvent
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public SexoEnum Sexo { get; set; }

        public int TelefonoMovil { get; set; }

        public string Email { get; set; }


        public ModificarJugadorEvent(int id, string nombre, SexoEnum sexo, int telefonoMovil, string email)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Sexo = sexo;
            this.TelefonoMovil = telefonoMovil;
            this.Email = email;
        }
    }
}
