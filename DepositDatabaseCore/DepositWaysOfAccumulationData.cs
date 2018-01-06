using System.Collections.Generic;
using System.Linq;
using DepositDatabaseCore.Model;

namespace DepositDatabaseCore
{
    public class DepositWaysOfAccumulationData
    {
        public static DepositWayOfAccumulation GetWayById(int wayId)
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetWayById(dbContext, wayId); 
            }
        }

        public static DepositWayOfAccumulation GetWayById(DepositDbContext dbContext, int wayId)
        {
            return dbContext.DepositWaysOfAccumulation.First(d => d.Id == wayId);
        }

        public static List<DepositWayOfAccumulation> GetList()
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetList(dbContext);
            }
        }

        public static List<DepositWayOfAccumulation> GetList(DepositDbContext dbContext)
        {
            return dbContext.DepositWaysOfAccumulation.ToList();
        }
    }
}
