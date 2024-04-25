using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace TravelWebProject.repo.ItinerariesRepo
{
    public class ItineraryRepo : IItineraryRepo
    {
        public List<Itinerary> GetItinerariesByTourId(int tourId) => ItineraryDAO.Instance.GetItinerariesByTourId(tourId);
        public void DeleteItinerary(int itineraryId) => ItineraryDAO.Instance.DeleteItinerary(itineraryId);
        public void UpdateItinerary(Itinerary itinerary) => ItineraryDAO.Instance.UpdateItinerary(itinerary);
        public Itinerary GetItineraryById(int itineraryId) => ItineraryDAO.Instance.GetItineraryById(itineraryId);
        public List<Itinerary> GetAllItineraries() => ItineraryDAO.Instance.GetAllItineraries();
        public void AddItinerary(Itinerary itinerary) => ItineraryDAO.Instance.AddItinerary(itinerary);
    }
}
