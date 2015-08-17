using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;
using DomainLogic;
using Microsoft.Practices.Unity;
using Cards = DomainLogic.Model.Cards;

namespace DepositDatabase
{
    public class AddCardHandler : IAddCardHandler
    {
        [Dependency]
        public IAddCardHandler DefaultAddCardHandler { get; set; }
        
        private DepositEntities db;

        public AddCardHandler()
        {
            db = new DepositEntities();
        }
        
        public Cards GetCardByRequisites(string id, string expirationMonth, string expirationYear, string secretCode)
        {
            var card = (from cards in db.Cards
                        where cards.Id == id
                              && cards.ExpirationMonth == expirationMonth
                              && cards.ExpirationYear == expirationYear
                              && cards.SecretCode == secretCode
                        select cards).First();
            return card.ToDomainLogicCard();
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }
    }


    public static class DepositDatabaseCardsExtension
    {
        public static DomainLogic.Model.Cards ToDomainLogicCard(this DepositDatabase.Model.Cards card)
        {
            return new Cards()
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
