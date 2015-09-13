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
            return DepositEntitiesExtension.GetInstance().DepositTerms.First(d => d.Id == id);
        }

        public static List<DepositTerms> GetList()
        {
            return DepositEntitiesExtension.GetInstance().DepositTerms.ToList();
        }
    }
}
