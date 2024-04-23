using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RegionDAO
    {
        private static RegionDAO instance = null;
        private readonly TravelWebContext context = null;

        private RegionDAO()
        {
            context = new TravelWebContext();
        }

        public static RegionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RegionDAO();
                }
                return instance;
            }
        }

        public List<Region> GetAll()
        {
            try
            {
                return context.Regions.ToList();
            }catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
