using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic.Model
{
    public interface ICardsService : IDbEntityService<Cards, string>
    {
        List<Cards> CreateUserCardsByCurrencyList(string userId, byte termsId);
    }
}
