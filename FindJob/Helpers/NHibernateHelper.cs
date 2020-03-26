using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace FindJob.Models
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure().Database(
                  MsSqlConfiguration.MsSql2012
                  .ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True")
                  .ShowSql())
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Role>())
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Education>())
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Experience>())
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Resume>())
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Vacancy>())
                  .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                  //.ExposeConfiguration(BuildSchema)
                  .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }

        //public ISession GetContextSession()
        //{
        //    var factory = GetFactory(); // GetFactory returns an ISessionFactory in my helper class
        //    ISession session;
        //    if (CurrentSessionContext.HasBind(factory))
        //    {
        //        session = factory.GetCurrentSession();
        //    }
        //    else
        //    {
        //        session = factory.OpenSession();
        //        CurrentSessionContext.Bind(session);
        //    }
        //    return session;
        //}

        //public void EndContextSession()
        //{
        //    var factory = GetFactory();
        //    var session = CurrentSessionContext.Unbind(factory);
        //    if (session != null && session.IsOpen)
        //    {
        //        try
        //        {
        //            if (session.Transaction != null && session.Transaction.IsActive)
        //            {
        //                session.Transaction.Rollback();
        //                throw new Exception("Rolling back uncommited NHibernate transaction.");
        //            }
        //            session.Flush();
        //        }
        //        catch (Exception ex)
        //        {
        //            log.Error("SessionKey.EndContextSession", ex);
        //            throw;
        //        }
        //        finally
        //        {
        //            session.Close();
        //            session.Dispose();
        //        }
        //    }
        //}


    }
}