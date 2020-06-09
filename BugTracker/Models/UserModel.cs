using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class UserModel
    {
        [Key]
        public int key { get; set; }

        // Will be same as the identity key
        [Required]
        public string userId { get; set; }
    }
}
