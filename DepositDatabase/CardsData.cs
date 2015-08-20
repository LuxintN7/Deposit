using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class CardsData
    {
        public static Cards GetCardById(string id)
        {
            using (var dbContext = new DepositEntities())
            {
                return GetCardById(id, dbContext);
            }
        }

        public static Cards GetCardById(string id, DepositEntities dbContext)
        {
            return dbContext.Cards.First(c => c.Id.Equals(id));
        }

        public static List<Cards> CreateUserCardsByCurrencyList(string userId, byte termsId)
        {
            List<Cards> cards;

            using (var dbContext = new DepositEntities())
            {
                var currencyId = DepositTermsData.GetTermsById(termsId, dbContext).CurrencyId;
                cards = CreateUserCardsByCurrencyList(userId, currencyId, dbContext);
            }

            return cards;
        }

        public static List<Cards> CreateUserCardsByCurrencyList(string userId, byte currencyId, DepositEntities dbContext)
        {
            List<Cards> cards = (from c in dbContext.Cards
                where c.AspNetUsers.Id == userId && c.Currencies.Id == currencyId
                select c).ToList();

            return cards;
        }

        public static List<Cards> GetList()
        {
            using (var db = new DepositEntities())
            {
                return db.Cards.ToList();
            }
        }
    }
}
