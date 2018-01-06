using System.Collections.Generic;

namespace DepositDatabaseCore
{
    public class DepositTermsService : DomainLogic.IDepositTermsService
    {
        public DomainLogic.Model.DepositTerm GetById(int id)
        {
            return DepositTermsData.GetTermById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.DepositTerm> GetList()
        {
            return DepositTermsData.GetList().ToDomainLogic();
        }
    }
}
