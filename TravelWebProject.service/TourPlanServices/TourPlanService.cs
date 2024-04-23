using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.TourPlanServices
{
    public class TourPlanService : ITourPlanService
    {
        private readonly TourPlanService _tourPlanService;
        public TourPlanService()
        {
            this._tourPlanService = new TourPlanService();
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
            _tourPlanService.CreateTourPlan(tourPlan);
        }
    }
}
