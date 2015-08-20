using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class CardsService : DomainLogic.Model.ICardsService
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
