using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Padel.Domain
{
    public class Role : Entity
    {
        public virtual string Name { get; set; }
    }
}
