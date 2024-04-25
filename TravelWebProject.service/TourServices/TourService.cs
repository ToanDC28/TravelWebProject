using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelWebProject.repo.Tours;
using TravelWebProject.repo.Users;

namespace TravelWebProject.service.TourServices
{
    public class TourService : ITourService
    {
        private readonly ITourRepo tourRepo;
        public TourService()
        {
            this.tourRepo = new TourRepo();
        }
        public List<Tour> GetAllTours()
        {
            return tourRepo.GetAllTours();
        }
        public Tour GetTourById(int tourId)
        {
            return tourRepo.GetTourById(tourId);
        }
        public void AddTour(Tour tour)
        {
            tourRepo.AddTour(tour);
        }
        public void UpdateTour(Tour tour)
        {
            tourRepo.UpdateTour(tour);
        }
        public void DeleteTour(int tourId)
        {
            tourRepo.DeleteTour(tourId);
        }
        public List<Tour> GetTourByDestinationId(int destinationId)
        {
            return tourRepo.GetTourByDestinationId(destinationId);
        }
    }
}
