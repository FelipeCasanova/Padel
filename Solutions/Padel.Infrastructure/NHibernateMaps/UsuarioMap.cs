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
    public class UsuarioMap : IAutoMappingOverride<Usuario>
    {
        public void Override(AutoMapping<Usuario> mapping)
        {
            mapping.Id(x => x.Id).UnsavedValue(0);
            mapping.Not.LazyLoad();
            mapping.Map(u => u.Nivel).ReadOnly();
            mapping.HasManyToMany<Role>(u => u.Roles).Cascade.All();
        }
    }
}
