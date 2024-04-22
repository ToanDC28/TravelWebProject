using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.Destinations
{
    public class DestinationRepository : IDestinationRepository
    {
        public void AddDestination(Destination destination) => DestinationDAO.Instance.AddDestination(destination);

        public void DeleteDestination(int id) => DestinationDAO.Instance.DeleteDestination(id);

        public Destination GetDestination(int id) => DestinationDAO.Instance.GetDestination(id);

        public List<Destination> GetDestinations() => DestinationDAO.Instance.GetDestinations();

        public void UpdateDestination(Destination destination) => DestinationDAO.Instance.UpdateDestination(destination);
    }
}
