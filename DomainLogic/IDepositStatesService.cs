using DomainLogic.Model;

namespace DomainLogic
{
    public interface IDepositStatesService : IDbEntityService<DepositStates, byte>
    {
        DepositStates GetByName(string name);
    }
}