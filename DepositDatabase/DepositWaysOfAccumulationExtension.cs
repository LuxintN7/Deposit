using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositWaysOfAccumulationExtension
    {
        public static DomainLogic.Model.DepositWaysOfAccumulation ToDomainLogic(
            this DepositWaysOfAccumulation wayOfAccumulation)
        {
            return new DomainLogic.Model.DepositWaysOfAccumulation()
            {
                Id = wayOfAccumulation.Id,
                Name = wayOfAccumulation.Name
            };
        }

        public static List<DomainLogic.Model.DepositWaysOfAccumulation> ToDomainLogic(this List<DepositWaysOfAccumulation> list)
        {
            var domainLogicWays = new List<DomainLogic.Model.DepositWaysOfAccumulation>();

            foreach (var way in list)
            {
                domainLogicWays.Add(way.ToDomainLogic());
            }

            return domainLogicWays;
        }
    }
}
