using System.Collections.Generic;
using System.Linq;
using DepositDatabaseCore.Model;

namespace DepositDatabaseCore
{
    public static class DepositStatesData
    {
        public static DepositState GetById(int id)
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetById(dbContext, id);
            }
        }

        public static DepositState GetById(DepositDbContext dbContext, int id)
        {
            return dbContext.DepositStates.First(d => d.Id == id);
        }

        public static DepositState GetStateByName(string name)
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetStateByName(dbContext, name); 
            }
        }

        // Required for InterestPaymentService
        public static DepositState GetStateByName(DepositDbContext dbContext, string name)
        {
            return dbContext.DepositStates.First(ds => ds.Name.Equals(name));
        }

        public static List<DepositState> GetList()
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetList(dbContext);
            } 
        }

        public static List<DepositState> GetList(DepositDbContext dbContext)
        {
            return dbContext.DepositStates.ToList();
        }
    }
}
