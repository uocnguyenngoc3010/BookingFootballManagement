using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBlackListRepository
    {
        IList<BlackList> GetList();
        BlackList GetByID(int id);
        BlackList GetByCustomerID(int id);
        void Add(BlackList blackList);
        void Delete(BlackList blackList);
        void Update(BlackList blackList);
    }
}
