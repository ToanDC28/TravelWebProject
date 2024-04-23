using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelWebProject.service.RegionService
{
    public interface IRegionService
    { 

        public List<Region> GetAll();
    }
}
