using FindJob.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindJob.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string result = "Войдите или зарегистрируйтесь.";

            if (User.Identity.IsAuthenticated)
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    User user = session.Query<User>().FirstOrDefault(u => u.Email == User.Identity.Name);
                    result = "Привет, " + user.FirstName + " " + user.LastName + "!";
                }
            }

            ViewBag.Message = result;
            return View();
        }

    }
}