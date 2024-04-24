using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.BookingService
{
    public class BookingService: IBookingService

    {
        public void Create(Booking booking) => BookingDAO.Instance.Create(booking);
        public Booking GetById(int bookingId) => BookingDAO.Instance.GetById(bookingId);
    }
}
