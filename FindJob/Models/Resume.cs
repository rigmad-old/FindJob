using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindJob.Models
{
    public class Resume
    {
        [Required]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Должность")]
        public virtual string Name { get; set; }

        [Range(0, 1000000000, ErrorMessage = "Введите сумму от 0 до 1 000 000 000")]
        [Display(Name = "Зарплата")]
        public virtual int? Salary { get; set; }

        [Display(Name = "Соискатель")]
        public virtual User User { get; set; }

        [Display(Name = "Опыт работы")]
        public virtual Experience Experience { get; set; }

        [Display(Name = "Образование")]
        public virtual Education Education { get; set; }

        [Display(Name = "Активно")]
        public virtual bool Enabled { get; set; }

        [Display(Name = "Дата")]
        [DisplayFormat(DataFormatString = "{0:M}")]
        public virtual DateTime Date { get; set; }
    }
}