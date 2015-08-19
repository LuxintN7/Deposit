using System;
using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class CardsExtension
    {
        public static DomainLogic.Model.Cards ToDomainLogic(this Cards card)
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

        public static List<DomainLogic.Model.Cards> ToDomainLogic(this List<Cards> list)
        {
            var domainLogicCards = new List<DomainLogic.Model.Cards>();

            foreach (var card in list)
            {
                domainLogicCards.Add(card.ToDomainLogic());
            }

            return domainLogicCards;
        }
    }
}