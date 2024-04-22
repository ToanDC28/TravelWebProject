using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DestinationDAO
    {
        private static DestinationDAO instance = null!;
        private readonly TravelWebContext context = null!;

        private DestinationDAO()
        {
            context = new TravelWebContext();
        }

        public static DestinationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DestinationDAO();
                }
                return instance;
            }
        }

        public Destination GetDestination(int id)
        {
            try
            {
                return context.Destinations
                    .Include("Region")
                    .Include("Tours")
                    .FirstOrDefault(p => p.DestinationId == id);
            }catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public List<Destination> GetDestinations()
        {
            try
            {
                return context.Destinations.Include("Region").Include("Tours").ToList();
            }catch  (Exception ex)
            {
                throw new Exception();
            }
        }

        public void AddDestination(Destination destination)
        {
            try
            {
                context.Destinations.Add(destination);
                context.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public void DeleteDestination(int id)
        {
            try
            {
                var d = context.Destinations.FirstOrDefault(p => p.DestinationId == id);
                if (d != null)
                {
                    context.Destinations.Remove(d);
                    context.SaveChanges();
                }
            }catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public void UpdateDestination(Destination destination)
        {
            try
            {
                var d = context.Destinations.FirstOrDefault(p => p.DestinationId == destination.DestinationId);
                if (d != null)
                {
                    d.Name = destination.Name;
                    d.Description = destination.Description;
                    d.Country = destination.Country;
                    d.Image = destination.Image;
                    d.RegionId = destination.RegionId;
                }
            }catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
