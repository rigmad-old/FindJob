using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using FindJob.Models;

namespace FindJob.Mappings
{
    public class ExperienceMap : ClassMap<Experience>
    {
        public ExperienceMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.Name);
            HasMany(x => x.Resumes)
                  .Inverse()
                  .Cascade.All();
            HasMany(x => x.Vacancies)
                .Inverse()
                .Cascade.All();
        }
    }
}