using System.Collections.Generic;

namespace DepositDatabaseCore
{
    public class CardsService : DomainLogic.ICardsService
    {
        public DomainLogic.Model.Card GetById(string id)
        {
            return CardsData.GetCardById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.Card> CreateUserCardsByCurrencyList(string userId, int termsId) 
        {
            return CardsData.CreateUserCardsByCurrencyList(userId, termsId).ToDomainLogic();
        }

        public List<DomainLogic.Model.Card> GetList()
        {
            return CardsData.GetList().ToDomainLogic();
        }
    }
}
