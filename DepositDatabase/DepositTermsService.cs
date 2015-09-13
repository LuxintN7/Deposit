using System.Collections.Generic;
using System.Diagnostics;

namespace DepositDatabase
{
    public class DepositTermsService : DomainLogic.IDepositTermsService
    {
        public DomainLogic.Model.DepositTerms GetById(byte id)
        {Debug.WriteLine("zzz1");
            return DepositTermsData.GetTermsById(id).ToDomainLogic();
            
        }

        public List<DomainLogic.Model.DepositTerms> GetList()
        {
            Debug.WriteLine("zzzz2");
            return DepositTermsData.GetList().ToDomainLogic();

        }
    }
}
