using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StaffRepository : IStaffRepository
    {
        public void Add(staff staff) => StaffDAO.Instance.Add(staff);

        public void Delete(staff staff) => StaffDAO.Instance.Delete(staff);

        public staff GetByEmail(string email) => StaffDAO.Instance.GetByEmail(email);

        public staff GetByID(int id) => StaffDAO.Instance.GetByID(id);

        public IList<staff> GetList() => StaffDAO.Instance.GetList();

        public string GetName(int id) => StaffDAO.Instance.GetName(id);

        public void Update(staff staff) => StaffDAO.Instance.Update(staff);
    }
}
