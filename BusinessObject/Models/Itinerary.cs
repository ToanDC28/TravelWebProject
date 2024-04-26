using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Itinerary
    {
        public int ItineraryId { get; set; }
        public int? TourId { get; set; }
        public int? DestinationId { get; set; }
        public int? Day { get; set; }
        public string Activity { get; set; }
        public int? TransportationModeId { get; set; }

        public virtual Destination Destination { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual TransportationMode TransportationMode { get; set; }
    }
}
