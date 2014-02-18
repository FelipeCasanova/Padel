using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Padel.Web.Mvc.Controllers.ViewModels.Torneos
{
    public class EquipoTorneoViewModel : TorneoViewModel
    {
        public int EquipoId { get; set; }

        public int EquipoCategoriaId { get; set; }
        
        public string EquipoNombre { get; set; }

        public bool EquipoPagado { get { return JugadorAPagado && JugadorBPagado; } }

        public int JugadorAId { get; set; }

        public string JugadorANombre { get; set; }

        public bool JugadorAPagado
        {
            get
            {
                return (Precio <= DineroFicticioJugadorA + DineroRealJugadorA)
                    || (2 * Precio <= DineroFicticioJugadorB + DineroRealJugadorB);
            }
        }

        //[JsonIgnore]
        public decimal DineroFicticioJugadorA { get; set; }

        //[JsonIgnore]
        public decimal DineroRealJugadorA { get; set; }

        public int JugadorBId { get; set; }

        public string JugadorBNombre { get; set; }

        public bool JugadorBPagado
        {
            get
            {

                return (Precio <= DineroFicticioJugadorB + DineroRealJugadorB)
                    || (2 * Precio <= DineroFicticioJugadorA + DineroRealJugadorA);
            }
        }

        [JsonIgnore]
        public decimal DineroFicticioJugadorB { get; set; }

        [JsonIgnore]
        public decimal DineroRealJugadorB { get; set; }

    }
}