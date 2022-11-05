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

        public IList<staff> GetList()
        {
            List<staff> staffs;
            var _context = new FBookingDBContext();
            staffs = _context.staff.ToList();
            return staffs;
        }

        public staff GetByID(int id)
        {
            staff staff = null;
            var _context = new FBookingDBContext();
            staff = _context.staff.FirstOrDefault(item => item.Id == id);
            return staff;
        }

        public staff GetByEmail(string email)
        {
            staff staff = null;
            var _context = new FBookingDBContext();
            staff = _context.staff.FirstOrDefault(item => item.Email == email);
            return staff;
        }

        public string GetName(int id)
        {
            staff staff = null;
            var _context = new FBookingDBContext();
            staff = _context.staff.FirstOrDefault(item => item.Id == id);
            return staff.Name;
        }
        public void Add(staff staff)
        {
            var _context = new FBookingDBContext();
            _context.staff.Add(staff);
            _context.SaveChangesAsync();
        }

        public void Update(staff staff)
        {
            var _context = new FBookingDBContext();
            _context.staff.Update(staff);
            _context.SaveChangesAsync();
        }
        public void Delete(staff staff)
        {
            var _context = new FBookingDBContext();
            _context.staff.Remove(staff);
            _context.SaveChangesAsync();
        }
    }
}

