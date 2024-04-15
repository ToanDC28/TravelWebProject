using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Destination
    {
        public Destination()
        {
            Itineraries = new HashSet<Itinerary>();
            ItineraryDetails = new HashSet<ItineraryDetail>();
            Tours = new HashSet<Tour>();
        }

        public int DestinationId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int RegionId { get; set; }

        public virtual Region Region { get; set; } = null!;
        public virtual ICollection<Itinerary> Itineraries { get; set; }
        public virtual ICollection<ItineraryDetail> ItineraryDetails { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
