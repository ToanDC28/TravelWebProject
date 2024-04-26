using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessObject
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new TravelWebContext(
                serviceProvider.GetRequiredService<DbContextOptions<TravelWebContext>>());

            // Kiểm tra xem dữ liệu đã tồn tại chưa
            if (context.Roles.Any())
            {
                return; // DB đã được seeded
            }

            context.Roles.AddRange(
                new Role
                {
                    Name = "ADMIN"
                },
                new Role
                {
                    Name = "USER"
                }
            );
            context.SaveChanges();
        }
    }
}