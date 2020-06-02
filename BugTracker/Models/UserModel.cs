using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace BugTracker.Models
{
    public class UserModel
    {
        // Use same Id as the userId found in the Identitiy class
        public int ? UserModelId { get; set; }

        [Required]
        [DisplayName("Full Name")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }


        // Projects created by said user
        [InverseProperty("UserCreatedProject")]
        public ICollection<Projects> AuthoredProjects { get; set; } = new Collection<Projects>();

        // Projects that the user has been invited to collaborate on
        [InverseProperty("UserAssignedProject")]
        public ICollection<Projects> AssignedProjects { get; set; } = new Collection<Projects>();

        [Required]
        public int numCompletedTickets { get; set; } = 0;

        [Required]
        public int numCompletedProjects { get; set; } = 0;

        // Completed Tickets
        public ICollection<Tickets> CompletedTickets { get; set; } = new Collection<Tickets>();

        [Required] // DataType(DataType.DateTime)
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy HH:mm:ss}")]
        public DateTime CreationDate { get; set; }


    }


}