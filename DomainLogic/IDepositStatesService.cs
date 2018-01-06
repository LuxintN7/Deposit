using DomainLogic.Model;

namespace DomainLogic
{
    public interface IDepositStatesService : IDbEntityService<DepositState, int>
    {
        DepositState GetByName(string name);
    }
}