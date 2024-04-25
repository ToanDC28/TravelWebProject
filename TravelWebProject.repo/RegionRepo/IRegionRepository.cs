using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.repo.RegionRepo
{
    public interface IRegionRepository
    {
        public List<Region> GetAll();
    }
}
