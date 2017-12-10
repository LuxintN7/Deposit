using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class DepositWaysOfAccumulationData
    {
        public static DepositWaysOfAccumulation GetWayById(byte wayId)
        {
            return DepositDbContextExtension.GetInstance().DepositWaysOfAccumulation.First(d => d.Id == wayId);
        }

        public static List<DepositWaysOfAccumulation> GetList()
        {
            return DepositDbContextExtension.GetInstance().DepositWaysOfAccumulation.ToList();
        }
    }
}
