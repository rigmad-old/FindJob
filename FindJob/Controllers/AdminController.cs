using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindJob.Models;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;


namespace FindJob.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult PartialUsers()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                List<User> users = session.Query<User>().ToList();
                foreach (var user in users)
                {
                    user.Password = "pass";
                }
                return View(users);
            }

        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session.Get<User>(id);
                user.Password = "pass";
                return View(user);
            }
        }

        //// GET: Admin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session.Query<User>().FirstOrDefault(u => u.Id == id);
                user.Password = "pass";
                ViewBag.Selectlist = GetAllRoles();
                return View(user);
            }
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User userEdited, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        var userToUpdate = session.Get<User>(id);

                        if (!String.IsNullOrEmpty(userEdited.Password))
                        {
                            userToUpdate.Password = GetEntities.CreateMD5Salt(userEdited.Password);
                        }

                        if (image != null)
                        {
                            userToUpdate.PhotoMimeType = image.ContentType;
                            userToUpdate.PhotoData = new byte[image.ContentLength];
                            image.InputStream.Read(userToUpdate.PhotoData, 0, image.ContentLength);
                        }

                        userToUpdate.FirstName = userEdited.FirstName;
                        userToUpdate.LastName = userEdited.LastName;
                        userToUpdate.PatronymicName = userEdited.PatronymicName;
                        userToUpdate.Phone = userEdited.Phone;
                        userToUpdate.Role.Id = userEdited.Role.Id;

                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(userToUpdate);
                            transaction.Commit();
                        }


                    }
                    return RedirectToAction("Index");

                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session.Get<User>(id);
                user.Password = "pass";
                return View(user);
            }
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(User user)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(user);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public FileContentResult GetImage(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session.Get<User>(id);
                if (user != null)
                {
                    return File(user.PhotoData, user.PhotoMimeType);
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<Role> GetAllRoles()
        { 
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var roles = session.Query<Role>().ToList<Role>();
                    return roles;
                }
        }
    }
}
