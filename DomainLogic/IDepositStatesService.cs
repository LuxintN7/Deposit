using DomainLogic.Model;

namespace DomainLogic
{
    public interface IDepositStatesService : IDbEntityService<DepositState, byte>
    {
        DepositState GetByName(string name);
    }
}