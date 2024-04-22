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
        public List<Tour> GetAllTours();
        public Tour GetTourById(int id);
        public void AddTour(Tour tour);
        public void UpdateTour(Tour tour);
        public void DeleteTour(int tourId);
    }
}
