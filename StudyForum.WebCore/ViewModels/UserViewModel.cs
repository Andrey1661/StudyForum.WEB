﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyForum.WebCore.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
