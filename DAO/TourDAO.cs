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
        private static TravelWebContext _context = null;

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
            return _context.Tours.Include("Destinate").Include("TourPlans").ToList();
        }

        // Phương thức để lấy một tour theo ID
        public Tour GetTourById(int tourId)
        {
            return _context.Tours.Include("Destinate").Include("TourPlans").FirstOrDefault(p => p.TourId == tourId);
        }

        // Phương thức để thêm một tour mới
        public void AddTour(Tour tour)
        {
            _context.Tours.Add(tour);
            _context.SaveChanges(true);
        }

        // Phương thức để cập nhật thông tin của một tour
        public void UpdateTour(Tour tour)
        {
            _context.Tours.Update(tour);
            _context.SaveChanges(true);
        }

        // Phương thức để xóa một tour
        public void DeleteTour(int tourId)
        {
            var tour = _context.Tours.FirstOrDefault(p => p.TourId == tourId);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
                _context.SaveChanges(true);
            }
        }
    }
}
