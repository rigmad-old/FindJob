using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindJob.Models
{
    public class User
    {
        public virtual int Id { get; set; }

        [Display(Name = "Email")]
        public virtual string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Display(Name = "Имя")]
        public virtual string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public virtual string LastName { get; set; }

        [Display(Name = "Отчество")]
        public virtual string PatronymicName { get; set; }

        [Display(Name = "Телефон")]
        public virtual string Phone { get; set; }

        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public virtual DateTime BirthDay { get; set; }

        [Display(Name = "Фото")]
        public virtual byte[] PhotoData { get; set; }

        public virtual string PhotoMimeType { get; set; }

        public virtual IList<Resume> Resumes { get; set; }

        public virtual IList<Vacancy> Vacancies { get; set; }
        
        [Display(Name = "Роль")]
        public virtual Role Role { get; set; }

        public User()
        {
            Resumes = new List<Resume>();
            Vacancies = new List<Vacancy>();
        }


    }
}