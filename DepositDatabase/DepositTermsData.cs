using System;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class DepositTermsData : DomainLogic.Model.IDepositTermsService
    {
        public DomainLogic.Model.DepositTerms GetById(byte id)
        {
            return GetTermsById(id).ToDomainLogic();
        }

        public static DepositTerms GetTermsById(byte id)
        {
            using (var dbContext = new DepositEntities())
            {
                return GetTermsById(id, dbContext);
            }
        }

        public static DepositTerms GetTermsById(byte id, DepositEntities dbContext)
        {
            return dbContext.DepositTerms.First(d => d.Id == id);
        }
    }
}
