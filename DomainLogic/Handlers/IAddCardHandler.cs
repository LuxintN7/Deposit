using DomainLogic.Model;

namespace DomainLogic.Handlers
{
    public interface IAddCardHandler
    {
        Card GetCardByRequisites(string id, string expirationMonth, string expirationYear, string secretCode);
        void SetCardOwnerId(Card card, string userOwnerId);
    }
}
