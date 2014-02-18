using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Padel.Web.Mvc.Controllers.ViewModels
{
    public interface IPrecio
    {
        [DataType(DataType.Currency)]
        decimal Precio { get;  }
    }
}