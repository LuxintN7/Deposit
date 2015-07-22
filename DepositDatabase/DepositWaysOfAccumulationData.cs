using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class DepositWaysOfAccumulationData
    {
        public static List<DepositWaysOfAccumulation> CreateWaysOfAccumulationList()
        {
            List<DepositWaysOfAccumulation> waysOfAccumulations;

            using (var db = new DepositEntities())
            {
                waysOfAccumulations = db.DepositWaysOfAccumulation.ToList();
            }

            return waysOfAccumulations;
        }
    }
}
