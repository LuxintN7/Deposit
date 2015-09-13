using System.Collections.Generic;

namespace DepositDatabase
{
    public class CardsService : DomainLogic.ICardsService
    {
        public DomainLogic.Model.Cards GetById(string id)
        {
            return CardsData.GetCardById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.Cards> CreateUserCardsByCurrencyList(string userId, byte termsId) 
        {
            return CardsData.CreateUserCardsByCurrencyList(userId, termsId).ToDomainLogic();
        }

        public List<DomainLogic.Model.Cards> GetList()
        {
            return CardsData.GetList().ToDomainLogic();
        }
    }
}
