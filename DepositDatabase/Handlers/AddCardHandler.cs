using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase.Handlers
{
    public class AddCardHandler : DomainLogic.IAddCardHandler
    {      
        private readonly DepositEntities dbContext;

        public AddCardHandler()
        {
            dbContext = new DepositEntities();
        }
        
        public DomainLogic.Model.Cards GetCardByRequisites(string id, string expirationMonth, string expirationYear, string secretCode)
        {
            var cardToAdd = (from cards in dbContext.Cards
                        where cards.Id == id
                              && cards.ExpirationMonth == expirationMonth
                              && cards.ExpirationYear == expirationYear
                              && cards.SecretCode == secretCode
                        select cards).FirstOrDefault();
            return (cardToAdd == null) ? null : cardToAdd.ToDomainLogic();
        }

        public void SetCardOwnerId(DomainLogic.Model.Cards card, string userOwnerId)
        {
            var dbCard = dbContext.Cards.Find(card.Id);
            dbCard.UserOwnerId = userOwnerId;
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                if (dbContext.HasUnsavedChanges())
                {
                    dbContext.SaveChanges();
                }

                dbContext.Dispose();
            }
        }
    }
}
