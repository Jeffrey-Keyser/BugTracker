﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class UserModel
    {
        // Will be same as userIdentity ID
        [Key]
        public string key { get; set; }


    }
}
