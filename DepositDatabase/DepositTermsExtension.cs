using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositTermsExtension
    {
        public static DomainLogic.Model.DepositTerms ToDomainLogic(this DepositTerms terms)
        {
            return new DomainLogic.Model.DepositTerms()
            {
                Id = terms.Id,
                CurrencyId = terms.CurrencyId,
                InterestRate = terms.InterestRate,
                Name = terms.Name
            };
        }
    }
}
