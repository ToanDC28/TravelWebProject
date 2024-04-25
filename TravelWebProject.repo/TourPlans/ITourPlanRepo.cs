using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.TourPlans
{
    public interface ITourPlanRepo
    {
        public TourPlan GetTourPlanById(int planId);
        public List<TourPlan> GetAllTourPlans();
        public void DeleteTourPlan(int planId);
        public void UpdateTourPlan(TourPlan tourPlan);
        public void CreateTourPlan(TourPlan tourPlan);
        public List<TourPlan> GetTourPlansByTourId(int tourId);

    }
}
