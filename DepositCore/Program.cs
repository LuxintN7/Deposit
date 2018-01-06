using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DepositDatabaseCore.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DepositCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(null);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                
                try
                {
                    var dbContext = services.GetRequiredService<DepositDbContext>();

                    dbContext.Database.Migrate();

                    if (args.Length > 0 && args.Any(a => a == "populate") && !dbContext.Currencies.Any())
                    {
                        PopulateDbTablesWithData(dbContext);
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static void PopulateDbTablesWithData(DepositDbContext dbContext)
        {
            dbContext.Currencies.AddRange(new List<Currency>
            {
                new Currency {Name = "UAH", Abbreviation = "UAH"},
                new Currency {Name = "USD", Abbreviation = "USD"}
            });

            dbContext.SaveChanges();

            var uahCurrency = dbContext.Currencies.First(c => c.Abbreviation == "UAH");
            var usdCurrency = dbContext.Currencies.First(c => c.Abbreviation == "USD");

            dbContext.Cards.AddRange(new List<Card>
            {
                new Card
                {
                    Id = "0000000000000000",
                    SecretCode = "000",
                    Currency = uahCurrency,
                    ExpirationMonth = "10",
                    ExpirationYear = "20",
                    Balance = 100
                },
                new Card
                {
                    Id = "0000000000000001",
                    SecretCode = "001",
                    Currency = uahCurrency,
                    ExpirationMonth = "10",
                    ExpirationYear = "20",
                    Balance = 2500
                },
                new Card
                {
                    Id = "0000000000000010",
                    SecretCode = "010",
                    Currency = usdCurrency,
                    ExpirationMonth = "22",
                    ExpirationYear = "22",
                    Balance = 300
                },
                new Card
                {
                    Id = "0000000000000011",
                    SecretCode = "011",
                    Currency = usdCurrency,
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
                    Currency = uahCurrency,
                    InterestRate = 15,
                    Months = 3
                },
                new DepositTerm
                {
                    Name = "UAH 6",
                    Currency = uahCurrency,
                    InterestRate = 17,
                    Months = 6
                },
                new DepositTerm
                {
                    Name = "UAH 12",
                    Currency = uahCurrency,
                    InterestRate = 20,
                    Months = 12
                },
                new DepositTerm
                {
                    Name = "USD 3",
                    Currency = usdCurrency,
                    InterestRate = 3,
                    Months = 3
                },
                new DepositTerm
                {
                    Name = "USD 6",
                    Currency = usdCurrency,
                    InterestRate = 5,
                    Months = 6
                },
                new DepositTerm
                {
                    Name = "USD 12",
                    Currency = usdCurrency,
                    InterestRate = 8,
                    Months = 12
                },
            });

            dbContext.DepositWaysOfAccumulation.AddRange(new List<DepositWayOfAccumulation>()
            {
                new DepositWayOfAccumulation{Name = "Add to deposit balance (capitalization)"},
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
