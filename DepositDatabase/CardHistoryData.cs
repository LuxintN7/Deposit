using System;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class CardHistoryData
    {
        public static void AddRecordToDbContext(Card card, string description)
        {
            var newRecord = CreateRecord(card, description);
            DepositDbContextExtension.GetInstance().CardHistory.Add(newRecord);
        }
        
        // Required for InterestPaymentService
        public static void AddRecordToDbContext(Card card, string description, DepositDbContext dbContext)
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
                Card = card
            };

            return newRecord;
        }
    }
}
