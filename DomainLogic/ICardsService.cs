using System.Collections.Generic;
using DomainLogic.Model;

namespace DomainLogic
{
    public interface ICardsService : IDbEntityService<Cards, string>
    {
        List<Cards> CreateUserCardsByCurrencyList(string userId, byte termsId);
    }
}
