using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelWebProject.repo.Tours;
using TravelWebProject.repo.Transports;

namespace TravelWebProject.service.TransportServices
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepo transportRepo;
        public TransportService()
        {
            this.transportRepo = new TransportRepo();
        }
        public List<TransportationMode> GetAllTransportationModes()
        {
            return transportRepo.GetAllTransportationModes();
        }
        public TransportationMode GetTransportationModeById(int transportId)
        {
            return transportRepo.GetTransportationModeById(transportId);
        }
        public void AddTransportationMode(TransportationMode transport)
        {
            transportRepo.AddTransportationMode(transport);
        }
        public void UpdateTransportationMode(TransportationMode transport)
        {
            transportRepo.UpdateTransportationMode(transport);
        }
        public void DeleteTransportationMode(int transportId)
        {
            transportRepo.DeleteTransportationMode(transportId);
        }

    }
}
