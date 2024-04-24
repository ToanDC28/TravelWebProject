using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.BookingService
{
    public interface IBookingService
    {
        public Booking GetById(int bookingId);
        public void Create(Booking booking);
    }
}
