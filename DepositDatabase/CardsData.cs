using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class CardsData
    {
        public static Card GetCardById(string id) 
        {
            return DepositDbContextExtension.GetInstance().Cards.First(c => c.Id.Equals(id));
        }

        // Required for InterestPaymentService
        public static Card GetCardById(string id, DepositDbContext dbContext)
        {
            return dbContext.Cards.First(c => c.Id.Equals(id));
        }

        public static List<Card> CreateUserCardsByCurrencyList(string userId, int termsId)
        {
            var currencyId = DepositTermsData.GetTermById(termsId).CurrencyId;

            List<Card> cards = (from c in DepositDbContextExtension.GetInstance().Cards
                where c.UserOwner.Id == userId && c.Currency.Id == currencyId
                select c).ToList();

            return cards;
        }

        public static List<Card> GetList()
        {
            return DepositDbContextExtension.GetInstance().Cards.ToList();
        }
    }
}
