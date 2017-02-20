using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Padel.Domain;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;
using Padel.Domain.Operaciones;

namespace Padel.Infrastructure.NHibernateMaps
{
    public class OperacionMap : IAutoMappingOverride<Operacion>
    {
        public void Override(AutoMapping<Operacion> mapping)
        {
            mapping.Id(x => x.Id).UnsavedValue(0);
            mapping.DiscriminateSubClassesOnColumn("OperacionTipo");
        }
    }
}
