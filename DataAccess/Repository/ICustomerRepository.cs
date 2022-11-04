using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        IList<Customer> GetList();
        Customer GetByID(int id);
        Customer GetByEmail(string email);
        void Add(Customer customer);
        void Delete(Customer customer);
        void Update(Customer customer);
        string GetName(int id);
    }
}
