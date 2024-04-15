using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CustomerRepo
{
    public interface ICustomerRepository
    {
        public void DeleteCustomer(int id);
        public void Update(Customer customer);
        public Customer GetByEmail(string email);
        public List<Customer> GetAll();
        public Customer GetCustomerByID(int id);

    }
}
