using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FootballFieldDAO
    {
        private static FootballFieldDAO instance = null;
        private static readonly object instanceLock = new object();
        private FBookingDBContext _context;
        private FootballFieldDAO()
        {
            _context = new FBookingDBContext();
        }

        private string AVAILABLE_STATUS = "AVAILABLE";
        private string UNAVAILABLE_STATUS = "UNAVAILABLE";

        public static FootballFieldDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new FootballFieldDAO();
                    }
                    return instance;
                }
            }
        }

        public List<FootballField> GetAllAvailableFields()
        {
            List<FootballField> availableFields;
            var _context = new FBookingDBContext();
            availableFields = _context.FootballFields.Where(f => f.Status.Equals(AVAILABLE_STATUS)).ToList();

            return availableFields;
        }
        public IList<FootballField> GetList()
        {
            IList<FootballField> footballFields;
            var _context = new FBookingDBContext();
            footballFields = _context.FootballFields.ToList();
            return footballFields;
        }

        public FootballField GetByID(int id)
        {
            FootballField footballFields = null;
            var _context = new FBookingDBContext();
            footballFields = _context.FootballFields.FirstOrDefault(item => item.Id == id);
            return footballFields;
        }


        public void Add(FootballField footballFields)
        {
            var _context = new FBookingDBContext();
            _context.FootballFields.Add(footballFields);
            _context.SaveChangesAsync();
        }

        public void Update(FootballField footballFields)
        {
            var _context = new FBookingDBContext();
            _context.FootballFields.Update(footballFields);
            _context.SaveChangesAsync();
        }
        public void Delete(FootballField footballFields)
        {
            var _context = new FBookingDBContext();
            _context.FootballFields.Remove(footballFields);
            _context.SaveChangesAsync();
        }
    }
}
