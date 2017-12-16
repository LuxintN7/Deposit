using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositTermsExtension
    {
        public static DomainLogic.Model.DepositTerm ToDomainLogic(this DepositTerm terms)
        {
            return new DomainLogic.Model.DepositTerm()
            {
                Id = terms.Id,
                CurrencyId = terms.CurrencyId,
                InterestRate = terms.InterestRate,
                Name = terms.Name
            };
        }

        public static List<DomainLogic.Model.DepositTerm> ToDomainLogic(this List<DepositTerm> list)
        {
            var terms = new List<DomainLogic.Model.DepositTerm>();

            foreach (var term in list)
            {
                terms.Add(term.ToDomainLogic());
            }

            return terms;
        }
    }
}
