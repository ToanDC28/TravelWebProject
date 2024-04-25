using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Tour
    {
        public Tour()
        {
            Bookings = new HashSet<Booking>();
            Itineraries = new HashSet<Itinerary>();
            Reviews = new HashSet<Review>();
            TourPlans = new HashSet<TourPlan>();
        }

        public int TourId { get; set; }
        public string TourName { get; set; } = null!;
        public int DestinateId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Duration { get; set; } = null!;
        public string? TotalRating { get; set; }
        public decimal TotalCost { get; set; }
        public int? MinAge { get; set; }
        public int? TransportId { get; set; }

        public virtual Destination Destinate { get; set; } = null!;
        public virtual TransportationMode? Transport { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Itinerary> Itineraries { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TourPlan> TourPlans { get; set; }
    }
}
