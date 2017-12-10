using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class CardsData
    {
        public static Cards GetCardById(string id) 
        {
            return DepositDbContextExtension.GetInstance().Cards.First(c => c.Id.Equals(id));
        }

        // Required for InterestPaymentService
        public static Cards GetCardById(string id, DepositDbContext dbContext)
        {
            return dbContext.Cards.First(c => c.Id.Equals(id));
        }

        public static List<Cards> CreateUserCardsByCurrencyList(string userId, byte termsId)
        {
            var currencyId = DepositTermsData.GetTermsById(termsId).CurrencyId;

            List<Cards> cards = (from c in DepositDbContextExtension.GetInstance().Cards
                where c.AspNetUsers.Id == userId && c.Currencies.Id == currencyId
                select c).ToList();

            return cards;
        }

        public static List<Cards> GetList()
        {
            return DepositDbContextExtension.GetInstance().Cards.ToList();
        }
    }
}
