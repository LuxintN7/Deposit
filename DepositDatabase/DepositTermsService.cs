using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositDatabase
{
    public class DepositTermsService : DomainLogic.Model.IDepositTermsService
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
