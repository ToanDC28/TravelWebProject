using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.Transports
{
    public class TransportRepo : ITransportRepo
    {
        public void DeleteTransportationMode(int id)
        {
            TransportDAO.Instance.DeleteTransportationMode(id);
        }
        public void UpdateTransportationMode(TransportationMode transportationMode)
        {
            TransportDAO.Instance.UpdateTransportationMode(transportationMode);
        }
        public TransportationMode GetTransportationModeById(int id)
        {
            return TransportDAO.Instance.GetTransportationModeById(id);
        }
        public List<TransportationMode> GetAllTransportationModes()
        {
            return TransportDAO.Instance.GetAllTransportationModes();
        }
        public void AddTransportationMode(TransportationMode transportationMode)
        {
            TransportDAO.Instance.AddTransportationMode(transportationMode);
        }
    }
}
