using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace FindJob
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    //protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
    //{
    //    string rolename = string.Empty;
    //    if (FormsAuthentication.CookiesSupported == true)
    //    {
    //        if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
    //        {
    //            try
    //            {
    //                string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
    //                string roles = string.Empty;

    //                using (UserEntities entities = new UserEntities())
    //                {
    //                    var roleid = entities.tblUsers.Where(u => u.UserName == username).Select(u => u.RoleId);

    //                    int role = 0;
    //                    foreach (int i in roleid)
    //                    {
    //                        role = i;
    //                    }

    //                    rolename = entities.tblRoles.Where(r => r.Id == role).Select(r => r.RoleName).First().ToString();
    //                }
    //                e.User = new System.Security.Principal.GenericPrincipal(//, rolename.Split(';')); for more than one role
    //                   new System.Security.Principal.GenericIdentity(username, "Forms"), new String[] { rolename });
    //            }
    //            catch (Exception)
    //            {
    //                //somehting went wrong
    //            }
    //        }
    //    }
    //}


}
