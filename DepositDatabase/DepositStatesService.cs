namespace DepositDatabase
{
    public class DepositStatesService : DomainLogic.IDepositStatesService
    {
        public DomainLogic.Model.DepositStates GetById(byte id)
        {
            return DepositStatesData.GetById(id).ToDomainLogic();
        }

        public DomainLogic.Model.DepositStates GetByName(string name)
        {
            return DepositStatesData.GetStateByName(name).ToDomainLogic();
        }

        public System.Collections.Generic.List<DomainLogic.Model.DepositStates> GetList()
        {
            return DepositStatesData.GetList().ToDomainLogic();
        }

    }
}