using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindJob.Models;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Linq;

namespace FindJob.Controllers
{
    [Authorize]
    public class ResumeController : Controller
    {
        public ActionResult PartialResumes()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //var monthsAgo = Sustray DateTime.Today.Date;
                int userId = GetEntities.GetId(User.Identity.Name);

                List<Resume> resumes = null;
                
                if (User.IsInRole("Jobseeker"))
                {
                    resumes = session.Query<Resume>()
                        .Where(r => r.User.Id == userId)
                        .Fetch(r => r.User).ToList();
                }

                if (User.IsInRole("Employee"))
                {
                    resumes = session.Query<Resume>()
                        .Where(r => r.Enabled == true)
                        .Fetch(r => r.User).ToList();
                }
                
                if (User.IsInRole("Admin"))
                {
                    resumes = session.Query<Resume>()
                        .Fetch(r => r.User).ToList();
                }

                return View(resumes);
            }
        }
        [Authorize(Roles = "Employee")]
        public ActionResult SearchForVacancy(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Vacancy vacancyForSearch = session.Get<Vacancy>(id);

                List<Resume> resumes = null;
                resumes = session.Query<Resume>()
                        .Where(r => r.Enabled == true)
                        .Where(r => r.Experience.Id >= vacancyForSearch.Experience.Id)
                        .Where(r => r.Education.Id >= vacancyForSearch.Education.Id)
                        .Where(r => r.Salary <= vacancyForSearch.Salary)
                        .Fetch(r => r.User).ToList();

                return View(resumes);
            }
        }

        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var resume = session.Get<Resume>(id);
                return View(resume);
            }
        }

        [Authorize(Roles = "Jobseeker")]
        public ActionResult Create()
        {
            ViewBag.SelectlistExperiences = GetEntities.GetAll<Experience>();
            ViewBag.SelectlistEducations = GetEntities.GetAll<Education>();
            return View();
        }

        [Authorize(Roles = "Jobseeker")]
        [HttpPost]
        public ActionResult Create(Resume resume)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            resume.Enabled = true;
                            resume.Date = DateTime.Now.Date;
                            resume.User =  session.Query<User>().FirstOrDefault(u => u.Email == User.Identity.Name);
                            session.Save(resume);
                            transaction.Commit();
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.SelectlistExperiences = GetEntities.GetAll<Experience>();
                    ViewBag.SelectlistEducations = GetEntities.GetAll<Education>();
                    return View();
                }
            }

            ViewBag.Message = "Данные введены неверно";

            ViewBag.SelectlistExperiences = GetEntities.GetAll<Experience>();
            ViewBag.SelectlistEducations = GetEntities.GetAll<Education>();
            return View();
        }

        [Authorize(Roles = "Jobseeker")]
        public ActionResult Edit(int id)
        {
            ViewBag.SelectlistExperiences = GetEntities.GetAll<Experience>();
            ViewBag.SelectlistEducations = GetEntities.GetAll<Education>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var resume = session.Get<Resume>(id);
                return View(resume);
            }

        }

        [Authorize(Roles = "Jobseeker")]
        [HttpPost]
        public ActionResult Edit(int id, Resume resume)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        var resumeToUpdate = session.Get<Resume>(id);
                        resumeToUpdate.Education = resume.Education;
                        resumeToUpdate.Experience = resume.Experience;
                        resumeToUpdate.Name = resume.Name;
                        resumeToUpdate.Salary = resume.Salary;
                        resumeToUpdate.Enabled = resume.Enabled;
                     
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(resumeToUpdate);
                            transaction.Commit();
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.SelectlistExperiences = GetEntities.GetAll<Experience>();
                    ViewBag.SelectlistEducations = GetEntities.GetAll<Education>();
                    return View();
                }
            }
            ViewBag.SelectlistExperiences = GetEntities.GetAll<Experience>();
            ViewBag.SelectlistEducations = GetEntities.GetAll<Education>();
            return View();
        }

        [Authorize(Roles = "Jobseeker")]
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                int userId = GetEntities.GetId(User.Identity.Name);
                var resume = session.Get<Resume>(id);
                return View(resume);
            }
        }

        [Authorize(Roles = "Jobseeker")]
        [HttpPost]
        public ActionResult Delete(Resume resume)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(resume);
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



    }
}
