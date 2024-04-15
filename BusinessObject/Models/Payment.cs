using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? BookingId { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentType { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
