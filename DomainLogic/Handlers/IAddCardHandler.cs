using System;
using DomainLogic.Model;

namespace DomainLogic.Handlers
{
    public interface IAddCardHandler
    {
        Cards GetCardByRequisites(string id, string expirationMonth, string expirationYear, string secretCode);
        void SetCardOwnerId(Cards card, string userOwnerId);
    }
}
