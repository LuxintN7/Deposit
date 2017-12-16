using System.Collections.Generic;

namespace DepositDatabase
{
    public class DepositWaysOfAccumulationService : DomainLogic.IDepositWaysOfAccumulationService
    {
        public DomainLogic.Model.DepositWayOfAccumulation GetById(byte id)
        {
            return DepositWaysOfAccumulationData.GetWayById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.DepositWayOfAccumulation> GetList()
        {
            return DepositWaysOfAccumulationData.GetList().ToDomainLogic();
        }
    }
}