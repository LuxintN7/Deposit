using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositStatesExtension
    {
        public static DomainLogic.Model.DepositState ToDomainLogic(this DepositState state)
        {
            return new DomainLogic.Model.DepositState()
            { 
               Id = state.Id,
               Name = state.Name
            };
        }

        public static List<DomainLogic.Model.DepositState> ToDomainLogic(this List<DepositState> list)
        {
            var states = new List<DomainLogic.Model.DepositState>();

            foreach (var state in list)
            {
                states.Add(state.ToDomainLogic());
            }

            return states;
        }
    }
}
