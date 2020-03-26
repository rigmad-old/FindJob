using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindJob.Models
{
    public class Vacancy
    {
        [Required]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Название")]
        public virtual string Name { get; set; }
        
        [Required]
        [StringLength(255)]
        [Display(Name = "Описание")]
        public virtual string Description { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Компания")]
        public virtual string Company { get; set; }

        [Range(0, 1000000000, ErrorMessage = "Введите сумму от 0 до 1 000 000 000")]
        [Display(Name = "Зарплата")]
        public virtual int? Salary { get; set; }

        [Display(Name = "Работодатель")]
        public virtual User User { get; set; }

        [Display(Name = "Опыт работы")]
        public virtual Experience Experience { get; set; }

        [Display(Name = "Образование")]
        public virtual Education Education { get; set; }

        [Display(Name = "Активно")]
        public virtual bool Enabled { get; set; }

        [Display(Name = "Дата")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public virtual DateTime Date { get; set; }
    }
}