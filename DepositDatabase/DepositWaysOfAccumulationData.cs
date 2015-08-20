using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class DepositWaysOfAccumulationData
    {
        public static DepositWaysOfAccumulation GetWayById(byte wayId)
        {
            using (var dbContext = new DepositEntities())
            {
                return GetWayById(wayId, dbContext);
            }
        }

        public static DepositWaysOfAccumulation GetWayById(byte wayId, DepositEntities dbContext)
        {
            return dbContext.DepositWaysOfAccumulation.First(d => d.Id == wayId);
        }

        public static List<DepositWaysOfAccumulation> GetList()
        {
            using (var db = new DepositEntities())
            {
                return db.DepositWaysOfAccumulation.ToList();
            }
        }
    }
}
