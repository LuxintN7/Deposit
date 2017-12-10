using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositStatesData
    {
        public static DepositStates GetById(byte id)
        {
            return DepositDbContextExtension.GetInstance().DepositStates.First(d => d.Id == id);
            
        }

        public static DepositStates GetStateByName(string name)
        {
            return DepositDbContextExtension.GetInstance().DepositStates.First(ds => ds.Name.Equals(name));
        }

        // Required for InterestPaymentService
        public static DepositStates GetStateByName(string name, DepositDbContext dbContext)
        {
            return dbContext.DepositStates.First(ds => ds.Name.Equals(name));
        }

        public static List<DepositStates> GetList()
        {
            return DepositDbContextExtension.GetInstance().DepositStates.ToList();
        }
    }
}
