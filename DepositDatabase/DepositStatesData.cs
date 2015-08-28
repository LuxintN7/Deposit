using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositStatesData
    {
        public static DepositStates GetById(byte id)
        {
            using (var dbContext = new DepositEntities())
            {
                return GetById(id, dbContext);
            }
        }

        public static DepositStates GetById(byte id, DepositEntities dbContext)
        {
            return dbContext.DepositStates.First(d => d.Id == id);
        }

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

        public static List<DepositStates> GetList()
        {
            using (var db = new DepositEntities())
            {
                return db.DepositStates.ToList();
            }
        }
    }
}
