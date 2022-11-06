using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static readonly object instanceLock = new object();
        private CustomerDAO() { }
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }
        public IList<Customer> GetList()
        {
            List<Customer> customers;
            var _context = new FBookingDBContext();
            customers = _context.Customers.ToList();
            return customers;
        }

        public Customer GetByID(int id)
        {
            Customer customer = null;
            var _context = new FBookingDBContext();
            customer = _context.Customers.FirstOrDefault(item => item.Id == id);
            return customer;
        }

        public Customer GetByEmail(string email)
        {
            Customer customer = null;
            var _context = new FBookingDBContext();
            customer = _context.Customers.FirstOrDefault(item => item.Email == email);
            return customer;
        }

        public string GetName(int id)
        {
            Customer customer = null;
            var _context = new FBookingDBContext();
            customer = _context.Customers.FirstOrDefault(item => item.Id == id);
            return customer.Name;
        }

        public void Add(Customer customer)
        {
            var _context = new FBookingDBContext();
            _context.Customers.Add(customer);
            _context.SaveChangesAsync();
        }

        public void Update(Customer customer)
        {
            var _context = new FBookingDBContext();
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
        public void Delete(Customer customer)
        {
            var _context = new FBookingDBContext();
            _context.Customers.Remove(customer);
            _context.SaveChangesAsync();
        }
    }
}

