namespace DepositDatabase
{
    public class DepositStatesService : DomainLogic.IDepositStatesService
    {
        public DomainLogic.Model.DepositState GetById(byte id)
        {
            return DepositStatesData.GetById(id).ToDomainLogic();
        }

        public DomainLogic.Model.DepositState GetByName(string name)
        {
            return DepositStatesData.GetStateByName(name).ToDomainLogic();
        }

        public System.Collections.Generic.List<DomainLogic.Model.DepositState> GetList()
        {
            return DepositStatesData.GetList().ToDomainLogic();
        }

    }
}