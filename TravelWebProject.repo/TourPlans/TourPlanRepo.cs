using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace TravelWebProject.repo.TourPlans
{
    public class TourPlanRepo : ITourPlanRepo
    {
        public TourPlan GetTourPlanById(int planId) => TourPlanDAO.Instance.GetTourPlanById(planId);
        public List<TourPlan> GetAllTourPlans() => TourPlanDAO.Instance.GetAllTourPlans();
        public void DeleteTourPlan(int planId) => TourPlanDAO.Instance.DeleteTourPlan(planId);
        public void UpdateTourPlan(TourPlan tourPlan) => TourPlanDAO.Instance.UpdateTourPlan(tourPlan);
        public void CreateTourPlan(TourPlan tourPlan) => TourPlanDAO.Instance.CreateTourPlan(tourPlan);
        public List<TourPlan> GetTourPlansByTourId(int tourId) => TourPlanDAO.Instance.GetTourPlansByTourId(tourId);
    }
}
