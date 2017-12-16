using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositWaysOfAccumulationExtension
    {
        public static DomainLogic.Model.DepositWayOfAccumulation ToDomainLogic(
            this DepositWayOfAccumulation wayOfAccumulation)
        {
            return new DomainLogic.Model.DepositWayOfAccumulation
            {
                Id = wayOfAccumulation.Id,
                Name = wayOfAccumulation.Name
            };
        }

        public static List<DomainLogic.Model.DepositWayOfAccumulation> ToDomainLogic(this List<DepositWayOfAccumulation> list)
        {
            var domainLogicWays = new List<DomainLogic.Model.DepositWayOfAccumulation>();

            foreach (var way in list)
            {
                domainLogicWays.Add(way.ToDomainLogic());
            }

            return domainLogicWays;
        }
    }
}
