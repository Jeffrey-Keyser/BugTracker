using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Ticket Name")]
        public string TicketName { get; set; }

        [Required]
        [DisplayName("Ticket Description")]
        public string TicketDesc { get; set; }

        [Required]
        [StringLength(100)]
        public string userId { get; set; }

        // Associate the tickets with a projects model
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public bool Completed { get; set; } = false;

        [Required] // DataType(DataType.DateTime)
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy HH:mm:ss}")]
        public DateTime CreationDate { get; set; }

        [Required]
        [EnumDataType(typeof(Priority))]
        public Priority TicketPriority { get; set; }


    }
}

public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}
