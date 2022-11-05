using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IFootballFieldRepository
    {
        public List<FootballField> GetAllAvailableFields();
        IList<FootballField> GetList();
        FootballField GetByID(int id);
        void Add(FootballField footballField);
        void Delete(FootballField footballField);
        void Update(FootballField footballField);
    }
}
