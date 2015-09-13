using System.Collections.Generic;

namespace DepositDatabase
{
    public class DepositTermsService : DomainLogic.IDepositTermsService
    {
        public DomainLogic.Model.DepositTerms GetById(byte id)
        {
            return DepositTermsData.GetTermsById(id).ToDomainLogic();
            
        }

        public List<DomainLogic.Model.DepositTerms> GetList()
        {
            return DepositTermsData.GetList().ToDomainLogic();
        }
    }
}
