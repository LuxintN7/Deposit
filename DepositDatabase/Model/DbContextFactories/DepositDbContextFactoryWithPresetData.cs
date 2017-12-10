using System.Collections.Generic;
using System.Data.Entity;

namespace DepositDatabase.Model.DbContextFactories
{
    public class DepositDbContextFactoryWithPresetData : IDepositDbContextFactory
    {
        public static DepositDbContext dbContext;

        public DepositDbContext CreateDbContext()
        {
            if (dbContext == null)
            {
                var context = new DepositDbContext();

                // Clear all records
                foreach (var currency in context.Currencies)
                {
                    context.Entry(currency).State = EntityState.Deleted;
                }
                foreach (var card in context.Cards)
                {
                    context.Entry(card).State = EntityState.Deleted;
                }
                foreach (var depositTerm in context.DepositTerms)
                {
                    context.Entry(depositTerm).State = EntityState.Deleted;
                }
                context.SaveChanges();

                // Add records again
                context.Currencies.AddRange(new List<Currencies>
                {
                    new Currencies {Id = 0, Abbreviation = "UAH"},
                    new Currencies {Id = 1, Abbreviation = "USD"}
                });
                context.Cards.AddRange(new List<Cards>
                {
                    new Cards
                    {
                        Id = "0000000000000000",
                        SecretCode = "000",
                        Currencies = Currencies.GetById(0, context),
                        ExpirationMonth = "10",
                        ExpirationYear = "20",
                        Balance = 100
                    },
                    new Cards
                    {
                        Id = "0000000000000001",
                        SecretCode = "001",
                        Currencies = Currencies.GetById(0, context),
                        ExpirationMonth = "10",
                        ExpirationYear = "20",
                        Balance = 2500
                    },
                    new Cards
                    {
                        Id = "0000000000000010",
                        SecretCode = "010",
                        Currencies = Currencies.GetById(1, context),
                        ExpirationMonth = "22",
                        ExpirationYear = "22",
                        Balance = 300
                    },
                    new Cards
                    {
                        Id = "0000000000000011",
                        SecretCode = "011",
                        Currencies = Currencies.GetById(1, context),
                        ExpirationMonth = "22",
                        ExpirationYear = "22",
                        Balance = 1000
                    }
                });
                context.DepositTerms.AddRange(new List<DepositTerms>
                {
                    new DepositTerms
                    {
                        Id = 0,
                        Name = "UAH 3",
                        Currencies = Currencies.GetById(0, context),
                        InterestRate = 15,
                        Months = 3
                    },
                    new DepositTerms
                    {
                        Id = 1,
                        Name = "UAH 6",
                        Currencies = Currencies.GetById(0, context),
                        InterestRate = 17,
                        Months = 6
                    },
                    new DepositTerms
                    {
                        Id = 2,
                        Name = "UAH 12",
                        Currencies = Currencies.GetById(0, context),
                        InterestRate = 20,
                        Months = 12
                    },
                    new DepositTerms
                    {
                        Id = 3,
                        Name = "USD 3",
                        Currencies = Currencies.GetById(1, context),
                        InterestRate = 3,
                        Months = 3
                    },
                    new DepositTerms
                    {
                        Id = 4,
                        Name = "USD 6",
                        Currencies = Currencies.GetById(1, context),
                        InterestRate = 5,
                        Months = 6
                    },
                    new DepositTerms
                    {
                        Id = 5,
                        Name = "USD 12",
                        Currencies = Currencies.GetById(1, context),
                        InterestRate = 8,
                        Months = 12
                    },
                });
                context.SaveChanges();

                return context;
            }

            return dbContext;
        }
    }
}