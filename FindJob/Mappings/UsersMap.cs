using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindJob.Models;
using FluentNHibernate.Mapping;

namespace FindJob.Mappings
{
    public class UsersMap : ClassMap<User>
    {
        public UsersMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.PatronymicName);
            Map(x => x.Phone);
            Map(x => x.BirthDay);
            Map(x => x.PhotoData).Length(int.MaxValue); 
            Map(x => x.PhotoMimeType);
            HasMany(x => x.Resumes)
                .Inverse()
                .Cascade.All();
            HasMany(x => x.Vacancies)
                .Inverse()
                .Cascade.All();
            References(x => x.Role).Not.LazyLoad();

        }
    }
}