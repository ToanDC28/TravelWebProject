using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.ItineraryServices
{
    public interface IItineraryService
    {
        public List<Itinerary> GetItinerariesByTourId(int tourId);
        public void DeleteItinerary(int itineraryId);
        public void UpdateItinerary(Itinerary itinerary);
        public Itinerary GetItineraryById(int itineraryId);
        public List<Itinerary> GetAllItineraries();
        public void AddItinerary(Itinerary itinerary);
    }
}
