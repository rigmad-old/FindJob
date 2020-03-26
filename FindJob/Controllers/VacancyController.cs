using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindJob.Models;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Linq;
using System.Data.SqlClient;
using System.Data;

namespace FindJob.Controllers
{
    [Authorize]
    public class VacancyController : Controller
    {
        public ActionResult PartialVacancies()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                List<Vacancy> vacancies = null;

                if (User.IsInRole("Admin"))
                {
                    int employeeId = GetEntities.GetId(User.Identity.Name);
                    vacancies = session.Query<Vacancy>()
                                    .ToList();
                }
                if (User.IsInRole("Employee"))
                {
                    int employeeId = GetEntities.GetId(User.Identity.Name);
                    vacancies = session.Query<Vacancy>()
                                    .Where(v => v.User.Id == employeeId)
                                    .ToList();
                }

                if (User.IsInRole("Jobseeker"))
                {
                    vacancies = session.Query<Vacancy>()
                                    .Where(v => v.Enabled == true)
                                    .Where(v => v.Date > DateTime.Today.AddDays(-30))
                                    .ToList();
                }
                return View(vacancies);
            }
        }
        public ActionResult SearchForResume(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Resume resumeForSearch = session.Get<Resume>(id);

                List<Vacancy> vacancies = session.Query<Vacancy>()
                                    .Where(v => v.Enabled == true)
                                    .Where(v => v.Date > DateTime.Today.AddDays(-30))
                                    .Where(v => v.Experience.Id <= resumeForSearch.Experience.Id)
                                    .Where(v => v.Education.Id <= resumeForSearch.Education.Id)
                                    .Where(v => v.Salary >= resumeForSearch.Salary)
                                    .ToList();

                return View(vacancies);
            }
        }

        // GET: Vacancy/Details/5
        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var vacancy = session.Get<Vacancy>(id);
                return View(vacancy);
            }
        }

        [Authorize(Roles = "Employee")]
        public ActionResult Create()
        {
            ViewBag.SelectlistExperiences = GetEntities.GetAll<Experience>();
            ViewBag.SelectlistEducations = GetEntities.GetAll<Education>();
            return View();
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public ActionResult Create(Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True"))
                    using (var cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = "InsertVacancyProcdure";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", vacancy.Name);
                        cmd.Parameters.AddWithValue("@Description", vacancy.Description);
                        cmd.Parameters.AddWithValue("@Company", vacancy.Company);
                        cmd.Parameters.AddWithValue("@Salary", vacancy.Salary);
                        cmd.Parameters.AddWithValue("@Enabled", true);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@Experience_id", vacancy.Experience.Id);
                        cmd.Parameters.AddWithValue("@Education_id", vacancy.Education.Id);
                        cmd.Parameters.AddWithValue("@User_id", GetEntities.GetId(User.Identity.Name));
                        cmd.ExecuteNonQuery();
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

        [Authorize(Roles = "Employee")]
        public ActionResult Edit(int id)
        {
            ViewBag.SelectlistExperiences = GetEntities.GetAll<Experience>();
            ViewBag.SelectlistEducations = GetEntities.GetAll<Education>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var vacancy = session.Get<Vacancy>(id);
                return View(vacancy);
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public ActionResult Edit(int id, Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        var vacancyToUpdate = session.Get<Vacancy>(id);
                        vacancyToUpdate.Education = vacancy.Education;
                        vacancyToUpdate.Experience = vacancy.Experience;
                        vacancyToUpdate.Name = vacancy.Name;
                        vacancyToUpdate.Company = vacancy.Company;
                        vacancyToUpdate.Salary = vacancy.Salary;
                        vacancyToUpdate.Enabled = vacancy.Enabled;

                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(vacancyToUpdate);
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

        [Authorize(Roles = "Employee")]
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var vacancy = session.Get<Vacancy>(id);
                return View(vacancy);
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public ActionResult Delete(Vacancy vacancy)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(vacancy);
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
    }
}
