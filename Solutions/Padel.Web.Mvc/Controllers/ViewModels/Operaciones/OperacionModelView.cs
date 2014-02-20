using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain.Operaciones;

namespace Padel.Web.Mvc.Controllers.ViewModels.Operaciones
{
    public class OperacionModelView
    {

        protected OperacionModelView(Operacion model)
        {
            Accion = model.Accion;
            Mensaje = model.Mensaje;
            FechaCreacion = model.FechaCreacion;
            FechaModificacion = model.FechaModificacion;
        }

        public static OperacionModelView Crear(Operacion model)
        {
            if (model is UsuarioOperacion)
                return UsuarioOperacionModelView.Crear((UsuarioOperacion)model);
            else if (model is EquipoOperacion)
                return EquipoOperacionModelView.Crear((EquipoOperacion)model);

            return new OperacionModelView(model);
        }


        public string Accion { get; set; }

        public string Mensaje { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
