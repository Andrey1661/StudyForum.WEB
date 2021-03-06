﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class ThemeModel
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime CreationDate { get; set; }
        public AuthorModel Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
