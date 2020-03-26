﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindJob.Models
{
    public class Education
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Resume> Resumes { get; set; }
        public virtual IList<Vacancy> Vacancies { get; set; }

        public Education()
        {
            Resumes = new List<Resume>();
            Vacancies = new List<Vacancy>();
        }
    }


}