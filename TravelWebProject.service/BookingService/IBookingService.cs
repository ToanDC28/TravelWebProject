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
        public User getUserFrombooking(int UserId);
        public List<Booking> GetAllByUser(int userId);
        public void Update(Booking booking);
    }
}
