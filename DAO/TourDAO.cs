using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TourDAO
    {
        private static TourDAO instance = null;
        private readonly TravelWebContext _context = null;

        public TourDAO()
        {
            _context = new TravelWebContext();
        }

        public static TourDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TourDAO();
                }
                return instance;
            }
        }

        // Phương thức để lấy danh sách các tour
        public List<Tour> GetAllTours()
        {
            try
            {
                return _context.Tours.Include("Destinate").Include("Transport").Include("Itineraries").Include("TourPlans").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tours: {ex.Message}");
                throw;
            }
        }

        // Phương thức để lấy một tour theo ID
        public Tour GetTourById(int tourId)
        {
            try
            {
                return _context.Tours.Include("Destinate").Include("Transport").Include("Itineraries").Include("TourPlans").FirstOrDefault(t => t.TourId == tourId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tour with ID {tourId}: {ex.Message}");
                throw;
            }
        }

        // Phương thức để thêm một tour mới
        public void AddTour(Tour tour)
        {
            try
            {
                _context.Tours.Add(tour);
                _context.SaveChanges(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding tour: {ex.Message}");
                throw;
            }
        }

        // Phương thức để cập nhật thông tin của một tour
        public void UpdateTour(Tour tour)
        {
            try
            {
                _context.Tours.Update(tour);
                _context.SaveChanges(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating tour: {ex.Message}");
                throw;
            }
        }

        // Phương thức để xóa một tour
        public void DeleteTour(int tourId)
        {
            try
            {
                var tour = _context.Tours.FirstOrDefault(t => t.TourId == tourId);
                if (tour != null)
                {
                    _context.Tours.Remove(tour);
                    _context.SaveChanges(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting tour with ID {tourId}: {ex.Message}");
                throw;
            }
        }
    }
}
