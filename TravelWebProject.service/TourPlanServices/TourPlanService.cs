using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelWebProject.repo.TourPlans;

namespace TravelWebProject.service.TourPlanServices
{
    public class TourPlanService : ITourPlanService
    {
        private readonly ITourPlanRepo _tourPlanService;
        public TourPlanService()
        {
            this._tourPlanService = new TourPlanRepo();
        }
        public TourPlan GetTourPlanById(int planId)
        {
            return _tourPlanService.GetTourPlanById(planId);
        }
        public List<TourPlan> GetAllTourPlans()
        {
            return _tourPlanService.GetAllTourPlans();
        }
        public void DeleteTourPlan(int planId)
        {
            _tourPlanService.DeleteTourPlan(planId);
        }
        public void UpdateTourPlan(TourPlan tourPlan)
        {
            _tourPlanService.UpdateTourPlan(tourPlan);
        }
        public void CreateTourPlan(TourPlan tourPlan)
        {
            this._tourPlanService.CreateTourPlan(tourPlan);
        }
        public List<TourPlan> GetTourPlansByTourId(int tourId)
        {
            return _tourPlanService.GetTourPlansByTourId(tourId);
        }
    }
}
