using System.Linq;
using DepositDatabaseCore.Model;

namespace DepositDatabaseCore.Handlers
{
    public class AddCardHandler : DomainLogic.Handlers.IAddCardHandler
    {      
        public DomainLogic.Model.Card GetCardByRequisites(string id, string expirationMonth, string expirationYear, string secretCode)
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetCardByRequisites(dbContext, id, expirationMonth, expirationYear, secretCode);
            }
        }

        public void SetCardOwnerId(DomainLogic.Model.Card card, string userOwnerId)
        {
            using (var dbContext = new DepositDbContext())
            {
                SetCardOwnerId(dbContext, card, userOwnerId);
            }
        }

        public DomainLogic.Model.Card GetCardByRequisites(DepositDbContext dbContext, string id, string expirationMonth, string expirationYear, string secretCode)
        {
            var cardToAdd = (from cards in dbContext.Cards
                where cards.Id == id
                      && cards.ExpirationMonth == expirationMonth
                      && cards.ExpirationYear == expirationYear
                      && cards.SecretCode == secretCode
                select cards).FirstOrDefault();
            return (cardToAdd == null) ? null : cardToAdd.ToDomainLogic();
        }

        public void SetCardOwnerId(DepositDbContext dbContext, DomainLogic.Model.Card card, string userOwnerId)
        {
            var dbCard = dbContext.Cards.Find(card.Id);
            dbCard.UserOwnerId = userOwnerId;
        }
    }
}
