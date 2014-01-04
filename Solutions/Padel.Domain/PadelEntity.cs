using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Padel.Domain
{
    public class PadelEntity : Entity
    {
        public virtual DateTime FechaCreacion { get; set; }

        public virtual DateTime FechaModificacion { get; set; }
    }
}
