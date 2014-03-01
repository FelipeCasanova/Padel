using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Padel.Domain;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;

namespace Padel.Infrastructure.NHibernateMaps
{
    public class CategoriaMap : IAutoMappingOverride<Categoria>
    {
        public void Override(AutoMapping<Categoria> mapping)
        {
            mapping.Map(u => u.NivelMin).ReadOnly();
            mapping.Map(u => u.NivelMax).ReadOnly();
        }
    }
}
