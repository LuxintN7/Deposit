using System;
using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class CardsExtension
    {
        public static DomainLogic.Model.Card ToDomainLogic(this Card card)
        {
            return new DomainLogic.Model.Card()
            {
                Id = String.Copy(card.Id),
                UserOwnerId = (card.UserOwnerId == null) ? null : String.Copy(card.UserOwnerId),
                Balance = card.Balance,
                ExpirationMonth = String.Copy(card.ExpirationMonth),
                ExpirationYear = String.Copy(card.ExpirationYear),
                SecretCode = String.Copy(card.SecretCode),
                CurrencyId = card.CurrencyId
            };
        }

        public static List<DomainLogic.Model.Card> ToDomainLogic(this List<Card> list)
        {
            var domainLogicCards = new List<DomainLogic.Model.Card>();

            foreach (var card in list)
            {
                domainLogicCards.Add(card.ToDomainLogic());
            }

            return domainLogicCards;
        }
    }
}