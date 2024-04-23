using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.Tours
{
    public interface ITourRepo
    {
        public void DeleteTour(int tourId);
        public void UpdateTour(Tour tour);
        public void AddTour(Tour tour);
        public Tour GetTourById(int tourId);
        public List<Tour> GetAllTours();

    }
}
