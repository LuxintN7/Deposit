using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class DepositWaysOfAccumulationData
    {
        public static DepositWayOfAccumulation GetWayById(int wayId)
        {
            return DepositDbContextExtension.GetInstance().DepositWaysOfAccumulation.First(d => d.Id == wayId);
        }

        public static List<DepositWayOfAccumulation> GetList()
        {
            return DepositDbContextExtension.GetInstance().DepositWaysOfAccumulation.ToList();
        }
    }
}
