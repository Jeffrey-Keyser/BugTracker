using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Tickets
    {

        [Key]
        public int TicketId { get; set; }

        [Required]
        public string TicketName { get; set; }

        [Required]
        public string TicketDesc { get; set; }

        [Required]
        [StringLength(100)]
        public string userId { get; set; }

        // Associate the tickets with a projects model
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public bool Completed { get; set; } = false;
    }
}
