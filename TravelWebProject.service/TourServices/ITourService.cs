﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.TourServices
{
    public interface ITourService
    {
        public void DeleteTour(int tourId);
        public void UpdateTour(Tour tour);
        public void AddTour(Tour tour);
        public Tour GetTourById(int tourId);
        public List<Tour> GetAllTours();
        public List<Tour> GetTourByDestinationId(int destinationId);
    }
}
