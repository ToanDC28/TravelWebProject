using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.TransportServices
{
    public interface ITransportService
    {
        public void DeleteTransportationMode(int id);
        public void UpdateTransportationMode(TransportationMode transportationMode);
        public TransportationMode GetTransportationModeById(int id);
        public List<TransportationMode> GetAllTransportationModes();
        public void AddTransportationMode(TransportationMode transportationMode);
    }
}
