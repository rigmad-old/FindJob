using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindJob.Models;
using FluentNHibernate.Mapping;

namespace FindJob.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.Name);
            HasMany(x => x.Users)
                .Inverse()
                .Cascade.All();
        }
    }
}