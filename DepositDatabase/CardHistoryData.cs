using System;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class CardHistoryData
    {
        public static void AddRecordToDbContext(Cards card, string description)
        {
            var newRecord = CreateRecord(card, description);
            DepositEntitiesExtension.GetInstance().CardHistory.Add(newRecord);
        }
        
        // Required for InterestPaymentService
        public static void AddRecordToDbContext(Cards card, string description, DepositEntities dbContext)
        {
            var newRecord = CreateRecord(card, description);
            dbContext.CardHistory.Add(newRecord);
        }

        public static CardHistory CreateRecord(Cards card, string description)
        {
            var newRecord = new CardHistory()
            {
                DateTime = DateTime.Now,
                Desription = description,
                Cards = card
            };

            return newRecord;
        }
    }
}
