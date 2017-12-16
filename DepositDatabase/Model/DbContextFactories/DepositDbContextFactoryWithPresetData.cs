using System.Collections.Generic;

namespace DepositDatabase.Model.DbContextFactories
{
    public class DepositDbContextFactoryWithPresetData : IDepositDbContextFactory
    {
        private static bool isDatabasePopulatedWithData;

        public DepositDbContext CreateDbContext()
        {
            var dbContext = new DepositDbContext();

            if (!isDatabasePopulatedWithData)
            {
                PopulateDbTablesWithData(dbContext);
                isDatabasePopulatedWithData = true;
            }

            return dbContext;
        }

        private static void PopulateDbTablesWithData(DepositDbContext dbContext)
        {
            dbContext.Currencies.AddRange(new List<Currency>
            {
                // these are temporary Ids 
                new Currency {Id = 0, Name = "UAH", Abbreviation = "UAH"},
                new Currency {Id = 1, Name = "USD", Abbreviation = "USD"}
            });

            dbContext.Cards.AddRange(new List<Card>
            {
                new Card
                {
                    Id = "0000000000000000",
                    SecretCode = "000",
                    Currency = Currency.GetById(0, dbContext),
                    ExpirationMonth = "10",
                    ExpirationYear = "20",
                    Balance = 100
                },
                new Card
                {
                    Id = "0000000000000001",
                    SecretCode = "001",
                    Currency = Currency.GetById(0, dbContext),
                    ExpirationMonth = "10",
                    ExpirationYear = "20",
                    Balance = 2500
                },
                new Card
                {
                    Id = "0000000000000010",
                    SecretCode = "010",
                    Currency = Currency.GetById(1, dbContext),
                    ExpirationMonth = "22",
                    ExpirationYear = "22",
                    Balance = 300
                },
                new Card
                {
                    Id = "0000000000000011",
                    SecretCode = "011",
                    Currency = Currency.GetById(1, dbContext),
                    ExpirationMonth = "22",
                    ExpirationYear = "22",
                    Balance = 1000
                }
            });

            dbContext.DepositTerms.AddRange(new List<DepositTerm>
            {
                new DepositTerm
                {
                    Name = "UAH 3",
                    Currency = Currency.GetById(0, dbContext),
                    InterestRate = 15,
                    Months = 3
                },
                new DepositTerm
                {
                    Name = "UAH 6",
                    Currency = Currency.GetById(0, dbContext),
                    InterestRate = 17,
                    Months = 6
                },
                new DepositTerm
                {
                    Name = "UAH 12",
                    Currency = Currency.GetById(0, dbContext),
                    InterestRate = 20,
                    Months = 12
                },
                new DepositTerm
                {
                    Name = "USD 3",
                    Currency = Currency.GetById(1, dbContext),
                    InterestRate = 3,
                    Months = 3
                },
                new DepositTerm
                {
                    Name = "USD 6",
                    Currency = Currency.GetById(1, dbContext),
                    InterestRate = 5,
                    Months = 6
                },
                new DepositTerm
                {
                    Name = "USD 12",
                    Currency = Currency.GetById(1, dbContext),
                    InterestRate = 8,
                    Months = 12
                },
            });

            dbContext.DepositWaysOfAccumulation.AddRange(new List<DepositWayOfAccumulation>()
            {
                new DepositWayOfAccumulation{Name = "Add to deposit sum"},
                new DepositWayOfAccumulation{Name = "Transfer to card"}
            });

            dbContext.DepositStates.AddRange(new List<DepositState>
            {
                new DepositState{Name = DomainLogic.Model.DepositState.OpenedDepositStateName},
                new DepositState{Name = DomainLogic.Model.DepositState.ClosedDepositStateName}
            });

        dbContext.SaveChanges();
        }
    }
}