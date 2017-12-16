using System.Collections.Generic;
using DomainLogic.Model;

namespace DomainLogic
{
    public interface ICardsService : IDbEntityService<Card, string>
    {
        List<Card> CreateUserCardsByCurrencyList(string userId, byte termsId);
    }
}
