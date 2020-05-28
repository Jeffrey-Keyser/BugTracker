using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;


// Used for displaying a project's ticket information in a graph form

namespace BugTracker.Models
{
    public class TicketChart
    {
		//DataContract for Serializing Data - required to serve in JSON format
		[DataContract]
		public class DataPoint
		{
			public DataPoint(string label, int y)
			{
				this.Label = label;
				this.Y = y;
			}

			//Explicitly setting the name to be used while serializing to JSON.
			[DataMember(Name = "label")]
			public string Label = "";

			//Explicitly setting the name to be used while serializing to JSON.
			[DataMember(Name = "y")]
			public Nullable<int> Y = null;
		}


	}
}

