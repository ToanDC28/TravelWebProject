using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelWebProject.repo.Destinations;
using TravelWebProject.repo.ItinerariesRepo;

namespace TravelWebProject.service.ItineraryServices
{
    public class ItineraryService : IItineraryService
    {
        private readonly IItineraryRepo itineraryRepository;

        public ItineraryService()
        {
            this.itineraryRepository = new ItineraryRepo();
        }
        public List<Itinerary> GetItinerariesByTourId(int tourId) => itineraryRepository.GetItinerariesByTourId(tourId);
        public void DeleteItinerary(int itineraryId)    => itineraryRepository.DeleteItinerary(itineraryId);
        public void UpdateItinerary(Itinerary itinerary) => itineraryRepository.UpdateItinerary(itinerary);
        public Itinerary GetItineraryById(int itineraryId) => itineraryRepository.GetItineraryById(itineraryId);
        public List<Itinerary> GetAllItineraries() => itineraryRepository.GetAllItineraries();
        public void AddItinerary(Itinerary itinerary) => itineraryRepository.AddItinerary(itinerary);
    }
}
