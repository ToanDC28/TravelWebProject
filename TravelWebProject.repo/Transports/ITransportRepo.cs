using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.Transports
{
    public interface ITransportRepo
    {
        public void DeleteTransportationMode(int id);
        public void UpdateTransportationMode(TransportationMode transportationMode);
        public TransportationMode GetTransportationModeById(int id);
        public List<TransportationMode> GetAllTransportationModes();
        public void AddTransportationMode(TransportationMode transportationMode);

    }
}
