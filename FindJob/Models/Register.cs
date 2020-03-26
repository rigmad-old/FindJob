using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindJob.Models
{


    public class RegisterModel
    {
        [Required]
        [Display(Name = "Email")]
        [StringLength(255)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [StringLength(255)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Имя")]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Фамилия")]
        public virtual string LastName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Отчество")]
        public virtual string PatronymicName { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Телефон")]
        public virtual string Phone { get; set; }


        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public virtual DateTime BirthDay { get; set; }

        [Display(Name = "Фото")]
        public virtual byte[] PhotoData { get; set; }
        public virtual string PhotoMimeType { get; set; }

        [Required]
        [Range(2, 3, ErrorMessage = "Недопустимый тип роли")]
        [Display(Name = "Роль")]
        public virtual int RoleId { get; set; }



    }
}