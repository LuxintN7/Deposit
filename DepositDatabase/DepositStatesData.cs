using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositStatesData
    {
        public static DepositState GetById(byte id)
        {
            return DepositDbContextExtension.GetInstance().DepositStates.First(d => d.Id == id);
            
        }

        public static DepositState GetStateByName(string name)
        {
            return DepositDbContextExtension.GetInstance().DepositStates.First(ds => ds.Name.Equals(name));
        }

        // Required for InterestPaymentService
        public static DepositState GetStateByName(string name, DepositDbContext dbContext)
        {
            return dbContext.DepositStates.First(ds => ds.Name.Equals(name));
        }

        public static List<DepositState> GetList()
        {
            return DepositDbContextExtension.GetInstance().DepositStates.ToList();
        }
    }
}
