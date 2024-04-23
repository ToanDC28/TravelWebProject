using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelWebProject.repo.RegionRepo;
using TravelWebProject.repo.Tours;
using TravelWebProject.service.RegionService;

namespace TravelWebProject.service.RegionServices
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository regionRepository;
        public RegionService()
        {
            this.regionRepository = new RegionRepository();
        }
        public List<Region> GetAll() => this.regionRepository.GetAll();
    }
}
