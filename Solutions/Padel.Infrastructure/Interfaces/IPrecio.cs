using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Padel.Infrastructure.Interfaces
{
    public interface IPrecio
    {
        [DataType(DataType.Currency)]
        decimal Precio { get;  }
    }
}