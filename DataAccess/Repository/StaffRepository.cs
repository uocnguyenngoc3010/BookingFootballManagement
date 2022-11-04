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
        public void Add(Staff staff) => StaffDAO.Instance.Add(staff);

        public void Delete(Staff staff) => StaffDAO.Instance.Delete(staff);

        public Staff GetByEmail(string email) => StaffDAO.Instance.GetByEmail(email);

        public Staff GetByID(int id) => StaffDAO.Instance.GetByID(id);

        public IList<Staff> GetList() => StaffDAO.Instance.GetList();

        public string GetName(int id) => StaffDAO.Instance.GetName(id);

        public void Update(Staff staff) => StaffDAO.Instance.Update(staff);
    }
}
