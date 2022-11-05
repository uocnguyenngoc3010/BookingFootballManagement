using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class FootballFieldRepository : IFootballFieldRepository
    {
        public List<FootballField> GetAllAvailableFields() => FootballFieldDAO.Instance.GetAllAvailableFields();
        public void Add(FootballField footballField) => FootballFieldDAO.Instance.Add(footballField);

        public void Delete(FootballField footballField) => FootballFieldDAO.Instance.Delete(footballField);

        public FootballField GetByID(int id) => FootballFieldDAO.Instance.GetByID(id);

        public IList<FootballField> GetList() => FootballFieldDAO.Instance.GetList();

        public void Update(FootballField footballField) => FootballFieldDAO.Instance.Update(footballField);
    }
}
