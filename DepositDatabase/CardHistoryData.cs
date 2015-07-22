using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class CardHistoryData
    {
        public static void AddCardHistoryRecordToDbContext(DepositEntities dbContext, Cards card, string description)
        {
            var newCardHistoryRecord = CreateCardHistoryRecord(card, description);
            dbContext.CardHistory.Add(newCardHistoryRecord);
        }

        public static CardHistory CreateCardHistoryRecord(Cards card, string description)
        {
            var newCardHistoryRecord = new CardHistory()
            {
                DateTime = DateTime.Now,
                Desription = description,
                Cards = card
            };

            return newCardHistoryRecord;
        }
    }
}
