using System.Collections.Generic;

namespace DepositDatabase
{
    public class DepositWaysOfAccumulationService : DomainLogic.IDepositWaysOfAccumulationService
    {
        public DomainLogic.Model.DepositWaysOfAccumulation GetById(byte id)
        {
            return DepositWaysOfAccumulationData.GetWayById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.DepositWaysOfAccumulation> GetList()
        {
            return DepositWaysOfAccumulationData.GetList().ToDomainLogic();
        }
    }
}