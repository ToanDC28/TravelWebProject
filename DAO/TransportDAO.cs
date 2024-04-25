using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TransportDAO
    {
        private static TransportDAO instance = null!;
        private readonly TravelWebContext _context = null;

        public TransportDAO()
        {
            _context = new TravelWebContext();
        }

        public static TransportDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransportDAO();
                }
                return instance;
            }
        }

        // Thêm mới một phương tiện vận chuyển
        public void AddTransportationMode(TransportationMode transportationMode)
        {
            try
            {
                _context.TransportationModes.Add(transportationMode);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ví dụ: log lỗi, thông báo người dùng, hoặc xử lý theo nhu cầu của ứng dụng
                Console.WriteLine($"Error adding transportation mode: {ex.Message}");
                throw; // Ném lại ngoại lệ để cho lớp gọi xử lý tiếp
            }
        }

        // Lấy danh sách các phương tiện vận chuyển
        public List<TransportationMode> GetAllTransportationModes()
        {
            try
            {
                return _context.TransportationModes.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting transportation modes: {ex.Message}");
                throw;
            }
        }

        // Lấy một phương tiện vận chuyển theo ID
        public TransportationMode GetTransportationModeById(int id)
        {
            try
            {
                return _context.TransportationModes.FirstOrDefault(p => p.TransportationModeId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting transportation mode with ID {id}: {ex.Message}");
                throw;
            }
        }

        // Cập nhật thông tin của một phương tiện vận chuyển
        public void UpdateTransportationMode(TransportationMode transportationMode)
        {
            try
            {
                _context.TransportationModes.Update(transportationMode);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating transportation mode: {ex.Message}");
                throw;
            }
        }

        // Xóa một phương tiện vận chuyển
        public void DeleteTransportationMode(int id)
        {
            try
            {
                var transportationMode = _context.TransportationModes.FirstOrDefault(p => p.TransportationModeId == id);
                if (transportationMode != null)
                {
                    _context.TransportationModes.Remove(transportationMode);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting transportation mode with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
