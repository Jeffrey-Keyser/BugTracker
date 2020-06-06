using BugTracker.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // TODO: FIX
        public virtual ICollection<Tickets> TicketList { get; set; } = new Collection<Tickets>();

        [Required]
        [Display(Name = "Project Programming Language")]
        public String projectLanguage { get; set; }

        public BugTrackerUser userCreatedProject { get; set; }
        public BugTrackerUser userAssignedProject { get; set; }
    }

}


public enum programmingLanguages{
    [Display(Name = "C#")]
    CSharp = 0,
    C,
    Java,
    Go,
    Python,
    JavaScript, // 5
    Ruby,
    Swift,
    PHP, 
    TypeScript, 
    SQL // 10
}
