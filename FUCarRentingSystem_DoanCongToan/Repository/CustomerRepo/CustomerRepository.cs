using BusinessObject.Models;
using DAO.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        public void DeleteCustomer(int id) => CustomerDAO.Instance.DeleteCustomer(id);

        public List<Customer> GetAll() => CustomerDAO.Instance.GetAll();

        public Customer GetByEmail(string email) => CustomerDAO.Instance.GetByEmail(email);

        public Customer GetCustomerByID(int id) => CustomerDAO.Instance.GetCustomerByID(id);
        public void Update(Customer customer) => CustomerDAO.Instance.Update(customer);
    }
}
