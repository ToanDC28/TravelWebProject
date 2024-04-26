using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class BookingDAO
    {

        private static BookingDAO instance = null!;
        private static  TravelWebContext context = null!;
        public BookingDAO()
        {
            context = new TravelWebContext();
        }
        public static BookingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingDAO();
                }
                return instance;
            }
        }

        public  List<Booking> GetAll()
        {
            try
            {
                return context.Bookings.Include(b=>b.User).Include(b => b.Tour).ToList();
            }
            catch
            {
                throw;
            }
        }
        public Booking GetById(int bookingId)
        {
            return context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }



        public List<Booking> GetAllByUser(int userId)
        {
            try
            {

                var bookings = context.Bookings.Where(b => b.UserId == userId).Include(b => b.Tour).Include(b => b.Tour.Destinate).ToList();
                return bookings;
            }
            catch
            {
                throw;
            }

        }
        public Booking GetBookingFullInfor(int bookingId)
        {
            try
            {
                // Truy vấn để tìm Booking với bookingId cụ thể, bao gồm Tour, Destinate, và User
                var booking = context.Bookings
                    .Where(b => b.BookingId == bookingId)
                    .Include(b => b.Tour)
                    .Include(b => b.Tour.Destinate)
                    .Include(b => b.User)
                    .FirstOrDefault(); // Lấy đối tượng Booking đầu tiên thỏa mãn điều kiện

                return booking; // Trả về đối tượng Booking đã tìm thấy
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                throw new Exception("An error occurred while retrieving the booking information.", ex);
            }

        }

        public void Create(Booking booking)
        {
            try
            {
                // Thêm đối tượng Booking vào cơ sở dữ liệu
                context.Bookings.Add(booking);
                context.SaveChanges(true);
            }
            catch
            {
                throw;
            }
            
        }



        public void Update(Booking booking)
        {
            var existingBooking = context.Bookings.FirstOrDefault(b => b.BookingId == booking.BookingId);
            if (existingBooking == null)
            {
                Console.WriteLine($"Booking ID {booking.BookingId} không tồn tại");
                return;
            }

            try
            {
                // Cập nhật thông tin booking
                existingBooking.UserId = booking.UserId;
                existingBooking.TourId = booking.TourId;
                existingBooking.BookingDate = booking.BookingDate;
                existingBooking.Status = booking.Status;
                existingBooking.PaidAmount = booking.PaidAmount;
                existingBooking.TotalAmount = booking.TotalAmount;
                existingBooking.RemainingAmount = booking.RemainingAmount;
                existingBooking.PaymentDeadline = booking.PaymentDeadline;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Lỗi khi cập nhật booking: {ex.Message}");
            }
        }

        public void Delete(int bookingId)
        {
            var booking = context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null)
            {
                context.Bookings.Remove(booking);
                context.SaveChanges();
            }
        }
        public User getUserFrombooking(int UserId)
        {
            return context.Users.FirstOrDefault(u => u.UserId == UserId) ?? null;
        }
    
    }
}
