using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BlackListDAO
    {
        private static BlackListDAO instance = null;
        private static readonly object instanceLock = new object();
        private BlackListDAO() { }
        public static BlackListDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BlackListDAO();
                    }
                    return instance;
                }
            }
        }
        public IList<BlackList> GetList()
        {
            List<BlackList> blackLists;
            var _context = new FBookingDBContext();
            blackLists = _context.BlackList.ToList();
            return blackLists;
        }

        public BlackList GetByID(int id)
        {
            BlackList blackLists = null;
            var _context = new FBookingDBContext();
            blackLists = _context.BlackList.FirstOrDefault(item => item.Id == id);
            return blackLists;
        }

        public BlackList GetByCustomerID(int id)
        {
            BlackList blackLists = null;
            var _context = new FBookingDBContext();
            blackLists = _context.BlackList.FirstOrDefault(item => item.CustomerId == id);
            return blackLists;
        }

        public void Add(BlackList blackLists)
        {
            var _context = new FBookingDBContext();
            _context.BlackList.Add(blackLists);
            _context.SaveChanges();
        }

        public void Update(BlackList blackLists)
        {
            var _context = new FBookingDBContext();
            _context.BlackList.Update(blackLists);
            _context.SaveChangesAsync();
        }
        public void Delete(BlackList blackLists)
        {
            var _context = new FBookingDBContext();
            _context.BlackList.Remove(blackLists);
            _context.SaveChangesAsync();
        }
    }
}

