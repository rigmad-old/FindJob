using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using FindJob.Models;

namespace FindJob.Mappings
{
    public class VacancyMap : ClassMap<Vacancy>
    {
        public VacancyMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Company);
            Map(x => x.Salary);
            References(x => x.User).Not.LazyLoad();
            References(x => x.Experience).Not.LazyLoad();
            References(x => x.Education).Not.LazyLoad(); ;
            Map(x => x.Enabled);
            Map(x => x.Date);
        }
    }
}