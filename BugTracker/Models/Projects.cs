using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string Author { get; set; }

    }
}
