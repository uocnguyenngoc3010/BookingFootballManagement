using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IStaffRepository
    {
        IList<Staff> GetList();
        Staff GetByID(int id);
        Staff GetByEmail(string email);
        void Add(Staff staff);
        void Delete(Staff staff);
        void Update(Staff staff);
        string GetName(int id);
    }
}
