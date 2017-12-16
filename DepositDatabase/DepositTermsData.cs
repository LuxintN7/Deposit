using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class DepositTermsData
    {
        public static DepositTerm GetTermById(byte id)
        {
            return DepositDbContextExtension.GetInstance().DepositTerms.First(d => d.Id == id);
        }

        public static List<DepositTerm> GetList()
        {
            return DepositDbContextExtension.GetInstance().DepositTerms.ToList();
        }
    }
}
