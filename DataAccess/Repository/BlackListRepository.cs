using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BlackListRepository : IBlackListRepository
    {
        public void Add(BlackList blackList) => BlackListDAO.Instance.Add(blackList);

        public void Delete(BlackList blackList) => BlackListDAO.Instance.Delete(blackList);

        public BlackList GetByID(int id) => BlackListDAO.Instance.GetByID(id);

        public BlackList GetByCustomerID(int id) => BlackListDAO.Instance.GetByCustomerID(id);

        public IList<BlackList> GetList() => BlackListDAO.Instance.GetList();

        public void Update(BlackList blackList) => BlackListDAO.Instance.Update(blackList);
    }
}
