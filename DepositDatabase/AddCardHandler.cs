using System;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class AddCardHandler : DomainLogic.IAddCardHandler
    {
        public DomainLogic.IAddCardHandler DefaultAddCardHandler { get; set; }
        
        private DepositEntities dbContext;

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
            return (cardToAdd == null) ? null : cardToAdd.ToDomainLogicCard();
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
                dbContext.Dispose();
            }
        }
    }
    

    public static class DepositDatabaseCardsExtension
    {
        public static DomainLogic.Model.Cards ToDomainLogicCard(this Cards card)
        {
            return new DomainLogic.Model.Cards()
            {
                Id = String.Copy(card.Id),
                UserOwnerId = String.Copy(card.UserOwnerId),
                Balance = card.Balance,
                ExpirationMonth = String.Copy(card.ExpirationMonth),
                ExpirationYear = String.Copy(card.ExpirationYear),
                SecretCode = String.Copy(card.SecretCode),
                CurrencyId = card.CurrencyId
            };
        }
    }

}
