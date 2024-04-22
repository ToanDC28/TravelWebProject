using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.Destinations
{
    public interface IDestinationRepository
    {
        public void UpdateDestination(Destination destination);
        public void DeleteDestination(int id);
        public void AddDestination(Destination destination);
        public Destination GetDestination(int id);
        public List<Destination> GetDestinations();
    }
}
