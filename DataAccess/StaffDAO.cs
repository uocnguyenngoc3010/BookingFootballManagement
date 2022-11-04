using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StaffDAO
    {
        private static StaffDAO instance = null;
        private static readonly object instanceLock = new object();
        private StaffDAO() { }
        public static StaffDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StaffDAO();
                    }
                    return instance;
                }
            }
        }

        public IList<Staff> GetList()
        {
            List<Staff> staffs;
            var _context = new FBookingDBContext();
            staffs = _context.Staff.ToList();
            return staffs;
        }

        public Staff GetByID(int id)
        {
            Staff staff = null;
            var _context = new FBookingDBContext();
            staff = _context.Staff.FirstOrDefault(item => item.Id == id);
            return staff;
        }

        public Staff GetByEmail(string email)
        {
            Staff staff = null;
            var _context = new FBookingDBContext();
            staff = _context.Staff.FirstOrDefault(item => item.Email == email);
            return staff;
        }

        public string GetName(int id)
        {
            Staff staff = null;
            var _context = new FBookingDBContext();
            staff = _context.Staff.FirstOrDefault(item => item.Id == id);
            return staff.Name;
        }
        public void Add(Staff staff)
        {
            var _context = new FBookingDBContext();
            _context.Staff.Add(staff);
            _context.SaveChangesAsync();
        }

        public void Update(Staff staff)
        {
            var _context = new FBookingDBContext();
            _context.Staff.Update(staff);
            _context.SaveChangesAsync();
        }
        public void Delete(Staff staff)
        {
            var _context = new FBookingDBContext();
            _context.Staff.Remove(staff);
            _context.SaveChangesAsync();
        }
    }
}

