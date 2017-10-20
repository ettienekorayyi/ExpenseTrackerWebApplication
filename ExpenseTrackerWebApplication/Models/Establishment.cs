using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTrackerWebApplication.Models
{
    public class Establishment
    {
        public int EstablishmentId { get; set; }
        public string EstablishmentName { get; set; }
        public string Description { get; set; }

        public Establishment() { }

        public Establishment(int pkId, string eName, string description)
        {
            EstablishmentId = pkId;
            EstablishmentName = eName;
            Description = description;
        }

        public int HiddenValue
        {
            get { return EstablishmentId; }
        }

        public override string ToString()
        {
            return EstablishmentName;
        }
    }
}