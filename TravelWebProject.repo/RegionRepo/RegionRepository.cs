using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.RegionRepo
{
    public class RegionRepository : IRegionRepository
    {
        public List<Region> GetAll() => RegionDAO.Instance.GetAll();
    }
}
