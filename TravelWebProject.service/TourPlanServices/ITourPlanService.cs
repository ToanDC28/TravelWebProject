using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.TourPlanServices
{
    public interface ITourPlanService
    {
        public TourPlan GetTourPlanById(int planId);
        public List<TourPlan> GetAllTourPlans();
        public void DeleteTourPlan(int planId);
        public void UpdateTourPlan(TourPlan tourPlan);
        public void CreateTourPlan(TourPlan tourPlan);
    }
}
