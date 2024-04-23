using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ItineraryDAO
    {
        private static ItineraryDAO instance = null;
        private readonly TravelWebContext _context = null;

        public ItineraryDAO()
        {
            _context = new TravelWebContext();
        }
        public static ItineraryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItineraryDAO();
                }
                return instance;
            }
        }

        // Create a new itinerary
        public void AddItinerary(Itinerary itinerary)
        {
            try
            {
                _context.Itineraries.Add(itinerary);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding itinerary: {ex.Message}");
                throw;
            }
        }

        // Retrieve all itineraries
        public List<Itinerary> GetAllItineraries()
        {
            try
            {
                return _context.Itineraries.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving all itineraries: {ex.Message}");
                throw;
            }
        }

        // Retrieve an itinerary by ID
        public Itinerary GetItineraryById(int itineraryId)
        {
            try
            {
                return _context.Itineraries.FirstOrDefault(t => t.ItineraryId == itineraryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving itinerary by ID: {ex.Message}");
                throw;
            }
        }

        // Update an existing itinerary
        public void UpdateItinerary(Itinerary itinerary)
        {
            try
            {
                _context.Entry(itinerary).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating itinerary: {ex.Message}");
                throw;
            }
        }

        // Delete an itinerary by ID
        public void DeleteItinerary(int itineraryId)
        {
            try
            {
                var itinerary = _context.Itineraries.FirstOrDefault(t => t.ItineraryId == itineraryId);
                if (itinerary != null)
                {
                    _context.Itineraries.Remove(itinerary);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting itinerary: {ex.Message}");
                throw;
            }
        }

        // Retrieve a list of itineraries by tour ID
        public List<Itinerary> GetItinerariesByTourId(int tourId)
        {
            try
            {
                return _context.Itineraries
                    .Where(i => i.TourId == tourId)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving itineraries by tour ID: {ex.Message}");
                throw;
            }
        }
    }
}
