using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class CardsData
    {
        public static List<Cards> CreateUserCardsByCurrencyList(string userId, byte termsId)
        {
            List<Cards> cards;

            using (var dbContext = new DepositEntities())
            {
                var currencyId = dbContext.DepositTerms.First(dt => dt.Id == termsId).CurrencyId;
                cards = CreateUserCardsByCurrencyList(userId, dbContext, currencyId);
            }

            return cards;
        }

        public static List<Cards> CreateUserCardsByCurrencyList(string userId, DepositEntities dbContext, byte currencyId)
        {
            List<Cards> cards = (from c in dbContext.Cards
                where c.AspNetUsers.Id == userId && c.Currencies.Id == currencyId
                select c).ToList();

            return cards;
        }
    }
}
