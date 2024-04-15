using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ItineraryDetail
    {
        public int ItineraryDetailId { get; set; }
        public int TourId { get; set; }
        public int DayNumber { get; set; }
        public string? Description { get; set; }
        public int? DestinationId { get; set; }
        public string? TransportationMode { get; set; }

        public virtual Destination? Destination { get; set; }
        public virtual Tour Tour { get; set; } = null!;
    }
}
