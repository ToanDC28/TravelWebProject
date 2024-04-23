using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public static List<Booking> GetAll()
        {
            try
            {
                return context.Bookings.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Booking GetById(int bookingId)
        {
            return context.Bookings.Include("Payment").FirstOrDefault(b => b.BookingId == bookingId);
        }

        public void Create(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Booking không được null");
            }

            try
            {
                context.Bookings.Add(booking);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Lỗi khi thêm booking: {ex.Message}");
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






    }
}
