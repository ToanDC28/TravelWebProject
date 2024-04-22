﻿using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.TourServices
{
    public class TourService : ITourService
    {
        public List<Tour> GetAllTours() => TourDAO.Instance.GetAllTours();
        public Tour GetTourById(int tourId) => TourDAO.Instance.GetTourById(tourId);
        public void AddTour(Tour tour) => TourDAO.Instance.AddTour(tour);
        public void UpdateTour(Tour tour) => TourDAO.Instance.UpdateTour(tour);
        public void DeleteTour(int tourId) => TourDAO.Instance.DeleteTour(tourId);
    }
}
