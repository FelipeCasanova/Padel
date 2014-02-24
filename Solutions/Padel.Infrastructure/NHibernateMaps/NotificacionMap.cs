using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Padel.Domain;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;
using Padel.Domain.Operaciones;
using Padel.Domain.Notificaciones;

namespace Padel.Infrastructure.NHibernateMaps
{
    public class NotificacionMap : IAutoMappingOverride<Notificacion>
    {
        public void Override(AutoMapping<Notificacion> mapping)
        {
            mapping.DiscriminateSubClassesOnColumn("NotificacionTipo");
        }
    }
}
