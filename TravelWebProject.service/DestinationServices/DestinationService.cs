using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelWebProject.repo.Destinations;

namespace TravelWebProject.service.DestinationServices
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository destinationRepository;

        public DestinationService()
        {
            this.destinationRepository = new DestinationRepository();
        }

        public void AddDestination(Destination destination) => destinationRepository.AddDestination(destination);

        public void DeleteDestination(int id) => destinationRepository.DeleteDestination(id);

        public Destination GetDestination(int id) => this.destinationRepository.GetDestination(id);

        public List<Destination> GetDestinations() => this.destinationRepository.GetDestinations();

        public void UpdateDestination(Destination destination) => this.destinationRepository.UpdateDestination(destination);
    }
}
