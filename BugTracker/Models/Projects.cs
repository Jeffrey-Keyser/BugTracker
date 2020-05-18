using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Projects
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        [StringLength(60, MinimumLength = 3)]
        public string ProjectName { get; set; }


        [Required]
        public string Author { get; set; }

        [Required]
        public string userId { get; set; }

        [Required]
        public virtual ICollection<Tickets> TicketList { get; set; }

    }
}
