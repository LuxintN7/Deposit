using System.Collections.Generic;
using System.Linq;
using DepositDatabaseCore.Model;

namespace DepositDatabaseCore
{
    public class CardsData
    {
        public static Card GetCardById(string id)
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetCardById(dbContext, id);
            }
        }

        // Required for InterestPaymentService
        public static Card GetCardById(DepositDbContext dbContext, string id)
        {
            return dbContext.Cards.First(c => c.Id.Equals(id));
        }

        public static List<Card> CreateUserCardsByCurrencyList(string userId, int termsId)
        {
            using (var dbContext = new DepositDbContext())
            {
                return CreateUserCardsByCurrencyList(dbContext, userId, termsId);
            }
        }

        public static List<Card> CreateUserCardsByCurrencyList(DepositDbContext dbContext, string userId, int termsId)
        {
            var currencyId = DepositTermsData.GetTermById(dbContext, termsId).CurrencyId;

            List<Card> cards = (from c in dbContext.Cards
                where c.UserOwner.Id == userId && c.Currency.Id == currencyId
                select c).ToList();

            return cards;
        }

        public static List<Card> GetList()
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetList(dbContext);
            }
            
        }

        public static List<Card> GetList(DepositDbContext dbContext)
        {
            return dbContext.Cards.ToList();
        }
    }
}
