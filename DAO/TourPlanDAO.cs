using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                var local = _context.Set<TourPlan>()
                                   .Local
                                   .FirstOrDefault(entry => entry.PlanId.Equals(tourPlan.PlanId));

                // Kiểm tra xem local có phải null không
                if (local != null)
                {
                    // Tách
                    _context.Entry(local).State = EntityState.Detached;
                }

                // Bây giờ bạn có thể đính kèm thực thể của mình
                _context.Entry(tourPlan).State = EntityState.Modified;

                // Lưu các thay đổi
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ví dụ: ghi log, thông báo lỗi, hoặc xử lý khác theo yêu cầu
                Console.WriteLine($"Error creating TourPlan: {ex.Message}");
                throw; // Chuyển tiếp ngoại lệ để các lớp gọi có thể xử lý tiếp
            }
        }

        // Phương thức cập nhật một TourPlan đã tồn tại
        public void UpdateTourPlan(TourPlan tourPlan)
        {
            try
            {
                _context.TourPlans.Update(tourPlan);
                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating TourPlan: {ex.Message}");
                throw;
            }
        }

        // Phương thức xóa một TourPlan
        public void DeleteTourPlan(int planId)
        {
            try
            {
                var tourPlan = _context.TourPlans.FirstOrDefault(p => p.PlanId == planId);
                if (tourPlan != null)
                {
                    _context.TourPlans.Remove(tourPlan);
                    _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting TourPlan: {ex.Message}");
                throw;
            }
        }

        // Phương thức lấy danh sách tất cả TourPlan
        public List<TourPlan> GetAllTourPlans()
        {
            try
            {
                return _context.TourPlans.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all TourPlans: {ex.Message}");
                throw;
            }
        }

        // Phương thức lấy một TourPlan theo Id
        public TourPlan GetTourPlanById(int planId)
        {
            try
            {
                return _context.TourPlans.FirstOrDefault(p => p.PlanId == planId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting TourPlan by Id: {ex.Message}");
                throw;
            }
        }

        // Phương thức lấy danh sách TourPlan theo TourId
        public List<TourPlan> GetTourPlansByTourId(int tourId)
        {
            try
            {
                return _context.TourPlans.Where(p => p.TourId == tourId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting TourPlans by TourId: {ex.Message}");
                throw;
            }
        }
    }
}
