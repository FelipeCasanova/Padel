using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain.Operaciones;

namespace Padel.Web.Mvc.Controllers.ViewModels.Operaciones
{
    public class EquipoOperacionModelView : OperacionModelView
    {
        private EquipoOperacionModelView(EquipoOperacion model)
            : base(model)
        {
        }

        public static EquipoOperacionModelView Crear(EquipoOperacion model)
        {
            return new EquipoOperacionModelView(model);
        }
    }
}