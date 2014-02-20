using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain.Operaciones;

namespace Padel.Web.Mvc.Controllers.ViewModels.Operaciones
{
    public class UsuarioOperacionModelView : OperacionModelView
    {
        private UsuarioOperacionModelView(UsuarioOperacion model)
            : base(model)
        {
        }

        public static UsuarioOperacionModelView Crear(UsuarioOperacion model)
        {
            return new UsuarioOperacionModelView(model);
        }
    }
}