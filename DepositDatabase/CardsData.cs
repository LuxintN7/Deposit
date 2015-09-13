using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class CardsData
    {
        public static Cards GetCardById(string id)
        {
            return DepositEntitiesExtension.GetInstance().Cards.First(c => c.Id.Equals(id));
        }

        // Required for InterestPaymentService
        public static Cards GetCardById(string id, DepositEntities dbContext)
        {
            return dbContext.Cards.First(c => c.Id.Equals(id));
        }

        public static List<Cards> CreateUserCardsByCurrencyList(string userId, byte currencyId)
        {
            List<Cards> cards = (from c in DepositEntitiesExtension.GetInstance().Cards
                where c.AspNetUsers.Id == userId && c.Currencies.Id == currencyId
                select c).ToList();

            return cards;
        }

        public static List<Cards> GetList()
        {
            return DepositEntitiesExtension.GetInstance().Cards.ToList();
        }
    }
}
