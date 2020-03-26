using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FindJob.Models;
using NHibernate;

namespace CustomRoleProviderApp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl = "/")
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (ModelState.IsValid)
                {
                    User user = session.Query<User>().FirstOrDefault(u => u.Email == model.Name && u.Password == GetEntities.CreateMD5Salt(model.Password));
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    }
                }
                return View(model);
            }
               
        }

        public ActionResult Register()
        {
            var list = new SelectList(new[]
                    {
                        new { Id = "2", Name = "Соискатель" },
                        new { Id = "3", Name = "Работодатель" },
                    }, "Id", "Name", 2);
            ViewBag.SelectlistRoles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, HttpPostedFileBase image = null)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (ModelState.IsValid)
                {
                    User user = session.Query<User>().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                    if (user == null)
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            string PhotoMimeType = null;
                            byte[] PhotoData = null;
                            if (image != null)
                            {
                                PhotoMimeType = image.ContentType;
                                PhotoData = new byte[image.ContentLength];
                                image.InputStream.Read(PhotoData, 0, image.ContentLength);
                            }

                            Role role = session.Get<Role>(model.RoleId);
                            User newUser = new User { Email = model.Email,
                                                    Password = GetEntities.CreateMD5Salt(model.Password),
                                                    FirstName = model.FirstName,
                                                    LastName = model.LastName,
                                                    PatronymicName = model.PatronymicName,
                                                    Phone = model.Phone,
                                                    BirthDay = model.BirthDay,
                                                    PhotoData = PhotoData,
                                                    PhotoMimeType = PhotoMimeType,
                                                    Role = role,
                            };
                            session.Save(newUser);
                            transaction.Commit();
                        }

                        user = session.Query<User>().Where(u => u.Email == model.Email && u.Password == GetEntities.CreateMD5Salt(model.Password)).FirstOrDefault();
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
                return View(model);
            }
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}