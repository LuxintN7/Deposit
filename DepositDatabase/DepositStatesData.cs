using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositStatesData
    {
        public static DepositStates GetStateByName(string name)
        {
            using (var dbContext = new DepositEntities())
            {
                return GetStateByName(name, dbContext);
            }
        }

        public static DepositStates GetStateByName(string name, DepositEntities dbContext)
        {
            return dbContext.DepositStates.First(ds => ds.Name.Equals(name));
        }
    }
}
