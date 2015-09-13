using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class DepositWaysOfAccumulationData
    {
        public static DepositWaysOfAccumulation GetWayById(byte wayId)
        {
            return DepositEntitiesExtension.GetInstance().DepositWaysOfAccumulation.First(d => d.Id == wayId);
        }

        public static List<DepositWaysOfAccumulation> GetList()
        {
            return DepositEntitiesExtension.GetInstance().DepositWaysOfAccumulation.ToList();
        }
    }
}
