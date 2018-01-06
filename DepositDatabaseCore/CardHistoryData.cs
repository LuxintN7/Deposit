using System;
using DepositDatabaseCore.Model;

namespace DepositDatabaseCore
{
    public static class CardHistoryData
    {
        public static void AddRecordToDbContext(Card card, string description)
        {
            using (var dbContext = new DepositDbContext())
            {
                AddRecordToDbContext(dbContext, card, description);
            }
        }
        
        // Required for InterestPaymentService
        public static void AddRecordToDbContext(DepositDbContext dbContext, Card card, string description)
        {
            var newRecord = CreateRecord(card, description);
            dbContext.CardHistory.Add(newRecord);
        }

        public static CardHistory CreateRecord(Card card, string description)
        {
            var newRecord = new CardHistory()
            {
                DateTime = DateTime.Now,
                Desription = description,
                CardId = card.Id
            };

            return newRecord;
        }
    }
}
