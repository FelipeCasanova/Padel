using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Padel.Infrastructure.NHibernateMaps.Conventions
{
    public class SubclassConvention : ISubclassConvention
    {
        public void Apply(ISubclassInstance instance)
        {
            // Use the short name of the type, not the full name
            instance.DiscriminatorValue(instance.EntityType.Name);
        }
    }
}
