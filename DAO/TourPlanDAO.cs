using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TourPlanDAO
    {
        private static TourPlanDAO instance = null;
        private readonly TravelWebContext _context = null;

        public TourPlanDAO()
        {
            _context = new TravelWebContext(); // Khởi tạo DbContext
        }

        public static TourPlanDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TourPlanDAO();
                }
                return instance;
            }
        }
        // Phương thức tạo mới một TourPlan
        public void CreateTourPlan(TourPlan tourPlan)
        {
            _context.TourPlans.Add(tourPlan);
            _context.SaveChanges(true); // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Phương thức cập nhật một TourPlan đã tồn tại
        public void UpdateTourPlan(TourPlan tourPlan)
        {
            _context.TourPlans.Update(tourPlan);
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Phương thức xóa một TourPlan
        public void DeleteTourPlan(int planId)
        {
            var tourPlan = _context.TourPlans.FirstOrDefault(p => p.PlanId == planId);
            if (tourPlan != null)
            {
                _context.TourPlans.Remove(tourPlan);
                _context.SaveChanges(true); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Phương thức lấy danh sách tất cả TourPlan
        public List<TourPlan> GetAllTourPlans()
        {
            return _context.TourPlans.ToList();
        }

        // Phương thức lấy một TourPlan theo Id
        public TourPlan GetTourPlanById(int planId)
        {
            return _context.TourPlans.FirstOrDefault(p => p.PlanId == planId);
        }
    }
}
