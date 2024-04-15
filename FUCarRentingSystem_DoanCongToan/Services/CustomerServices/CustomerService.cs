using BusinessObject.Models;
using Microsoft.Extensions.Configuration;
using Repository.CustomerRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService()
        {
            this.customerRepository = new CustomerRepository();
        }

        public void DeleteCustomer(int id)
        {
            customerRepository.DeleteCustomer(id);
        }

        public List<Customer> GetAll()
        {
            return customerRepository.GetAll();
        }

        public Customer GetByEmail(string email)
        {
            return customerRepository.GetByEmail(email);
        }

        public Customer GetCustomerByID(int id)
        {
            return customerRepository.GetCustomerByID(id);
        }

        public void Update(Customer customer)
        {
            customerRepository.Update(customer);
        }

        public Customer Login(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var adminEmail = config.GetSection("Admin")["Email"];
            var adminPassword = config.GetSection("Admin")["Password"];
            if(adminEmail == email && adminPassword == password)
            {
                return new Customer { Email = email, Password = password };
            }
            var cus = GetByEmail(email);
            if(cus.Password == password)
            {
                return cus;
            }
            return null;
        }
    }
}
