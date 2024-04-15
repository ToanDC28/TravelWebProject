using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class TransportationMode
    {
        public TransportationMode()
        {
            Itineraries = new HashSet<Itinerary>();
            Tours = new HashSet<Tour>();
        }

        public int TransportationModeId { get; set; }
        public string? Mode { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Itinerary> Itineraries { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
