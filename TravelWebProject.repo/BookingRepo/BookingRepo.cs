using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.BookingRepo
{
    public class BookingRepo : IBookingRepo
    {
        public void Create(Booking booking) => BookingDAO.Instance.Create(booking);
        public Booking GetById(int bookingId) => BookingDAO.Instance.GetById(bookingId);
        public User getUserFrombooking(int UserId) => BookingDAO.Instance.getUserFrombooking(UserId);
        public List<Booking> GetAllByUser(int userId) => BookingDAO.Instance.GetAllByUser(userId);
        public Booking GetBookingFullInfor(int bookingId) => BookingDAO.Instance.GetBookingFullInfor(bookingId);
        public  List<Booking> GetAll() => BookingDAO.Instance.GetAll();
    }
}
