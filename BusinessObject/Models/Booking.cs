using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
        }

        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public int? TourId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? Status { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public DateTime? PaymentDeadline { get; set; }

        public virtual Tour? Tour { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
