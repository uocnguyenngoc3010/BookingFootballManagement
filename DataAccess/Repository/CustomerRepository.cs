using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Add(Customer customer) => CustomerDAO.Instance.Add(customer);

        public void Delete(Customer customer) => CustomerDAO.Instance.Delete(customer);

        public Customer GetByEmail(string email) => CustomerDAO.Instance.GetByEmail(email);

        public Customer GetByID(int id) => CustomerDAO.Instance.GetByID(id);

        public IList<Customer> GetList() => CustomerDAO.Instance.GetList();

        public string GetName(int id) => CustomerDAO.Instance.GetName(id);

        public void Update(Customer customer) => CustomerDAO.Instance.Update(customer);
    }
}
