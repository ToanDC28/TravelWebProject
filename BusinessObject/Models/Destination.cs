using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Destination
    {
        public Destination()
        {
            Itineraries = new HashSet<Itinerary>();
            Tours = new HashSet<Tour>();
        }

        public int DestinationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Itinerary> Itineraries { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
