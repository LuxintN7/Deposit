using System.Collections.Generic;

namespace DepositDatabase
{
    public class DepositTermsService : DomainLogic.IDepositTermsService
    {
        public DomainLogic.Model.DepositTerm GetById(byte id)
        {
            return DepositTermsData.GetTermById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.DepositTerm> GetList()
        {
            return DepositTermsData.GetList().ToDomainLogic();
        }
    }
}
