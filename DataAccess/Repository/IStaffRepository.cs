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
        IList<staff> GetList();
        staff GetByID(int id);
        staff GetByEmail(string email);
        void Add(staff staff);
        void Delete(staff staff);
        void Update(staff staff);
        string GetName(int id);
    }
}
