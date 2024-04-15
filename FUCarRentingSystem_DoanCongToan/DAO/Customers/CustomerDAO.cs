using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Customers
{
    public class CustomerDAO
    {
        private static CustomerDAO instance= null;
        private static FUCarRentingManagementContext context = null;


        public CustomerDAO() { 
            context = new FUCarRentingManagementContext();
        }

        public static CustomerDAO Instance { 
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDAO();
                }
                return instance;
            }
        }

        public Customer GetCustomerByID(int id)
        {
            try
            {
                return context.Customers.FirstOrDefault(p => p.CustomerId == id);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Customer> GetAll()
        {
            return context.Customers.ToList();
        }
        public Customer GetByEmail(string email)
        {
            return context.Customers.FirstOrDefault(c => c.Email == email);
        }

        public void Update(Customer customer)
        {
            try
            {
                var cus = context.Customers.FirstOrDefault(p => p.CustomerId == customer.CustomerId);
                cus.CustomerStatus = customer.CustomerStatus;
                cus.CustomerName = customer.CustomerName;
                cus.Telephone = customer.Telephone;
                cus.CustomerBirthday = customer.CustomerBirthday;
                cus.Email = customer.Email;
                cus.Password = customer.Password;
                context.Entry(cus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCustomer(int id)
        {
            try
            {
                context.Customers.Remove(GetCustomerByID(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
