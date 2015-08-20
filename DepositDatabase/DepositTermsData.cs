using System;
using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class DepositTermsData
    {
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

        public static List<DepositTerms> GetList()
        {
            using (var db = new DepositEntities())
            {
                return db.DepositTerms.ToList();
            }
        }
    }
}
