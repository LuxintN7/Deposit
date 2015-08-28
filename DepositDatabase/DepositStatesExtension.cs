using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositStatesExtension
    {
        public static DomainLogic.Model.DepositStates ToDomainLogic(this DepositStates state)
        {
            return new DomainLogic.Model.DepositStates()
            { 
               Id = state.Id,
               Name = state.Name
            };
        }

        public static List<DomainLogic.Model.DepositStates> ToDomainLogic(this List<DepositStates> list)
        {
            var states = new List<DomainLogic.Model.DepositStates>();

            foreach (var state in list)
            {
                states.Add(state.ToDomainLogic());
            }

            return states;
        }
    }
}
