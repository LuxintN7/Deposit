using System;
using System.IO;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;
using DomainLogic.Handlers;
using DepositDatabaseCore.Handlers;
using DepositDatabaseCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json.Linq;

namespace InterestPaymentService
{
    public class InterestPaymentService 
    {
        private const int MaxPendingDays = 3;
        private const string appSettingsFilePath = "appsettings.json";
        private DateTime scheduledDateTime;
        private DateTime currentDateTime;

        private Logger logger;
        private Timer timer;

        private int intervalMinutes;
        private int TimerInterval => intervalMinutes * 60 * 1000;

        private IConfiguration configuration;
        private IServiceProvider serviceProvider;
        private double period;

        public InterestPaymentService(string[] args)
        {
            logger = new Logger(AppDomain.CurrentDomain.BaseDirectory + "log.txt");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(appSettingsFilePath);

            configuration = builder.Build();

            var services = new ServiceCollection()
                .AddTransient<IInterestPaymentHandler, InterestPaymentHandler>();
                
            if (bool.Parse(configuration["UseSQLite"]))
            {
                // Use SQLite fopr deploying on Linux
                services.AddDbContext<DepositDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnectionSQLite")));
                logger.WriteLine($"Added SQL Server to DbContext options; connectionString='{configuration.GetConnectionString("DefaultConnectionSQLite")}'");
            }
            else
            {
                // Use SQL Server by default
                services.AddDbContext<DepositDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString(
                            "DefaultConnection") /*, b => b.MigrationsAssembly("DepositDatabaseCore")*/));
                logger.WriteLine($"Added SQLite to DbContext options; connectionString='{configuration.GetConnectionString("DefaultConnectionSQLite")}'");
            }

            serviceProvider = services.BuildServiceProvider();

            Start();
        }

        protected void Start()
        {
            scheduledDateTime = DateTime.Parse(configuration["ScheduledDateTime"]);
            intervalMinutes = Int32.Parse(configuration["IntervalMinutes"]);

            timer = new Timer(TimerInterval);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();

            //TimerOnElapsed(null, null);

            logger.WriteLine("Interest Payment Service starts.");
        }

        protected void Stop()
        {
            timer.Stop();
            logger.WriteLine("Interest Payment Service stops.");
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            currentDateTime = DateTime.Now;

            if (currentDateTime >= scheduledDateTime)
            {
                RunInterestPayment();
                SaveScheduledDateTime(scheduledDateTime.AddDays(1));
            }
        }

        private void RunInterestPayment()
        {
            logger.WriteLine("Interest payment starts.");

            try
            {
                using (var handler = serviceProvider.GetService<IInterestPaymentHandler>())
                {
                    logger.WriteLine($"Resolved interface '{nameof(IInterestPaymentHandler)}'");

                    var today = DateTime.Now.Date;

                    var deposits = handler.GetActiveDeposits();
                    logger.WriteLine($"Found {deposits.Count} deposits");

                    foreach (var deposit in deposits)
                    {
                        logger.WriteLine($"Processing deposit Id='{deposit.Id}...'");

                        if (handler.GetDepositStateName(deposit.Id) == "Pending")
                        {
                            logger.WriteLine($"Started checking if deposit should be extended'");
                            // If a customer does not withdraw a deposit during the pending period 
                            // the deposit will be automatically extended for one more (the same) term
                            if (today - deposit.EndDate >= TimeSpan.FromDays(MaxPendingDays))
                            {
                                handler.SetDepositState(deposit.Id, "Extended");
                                handler.ExtendDeposit(deposit.Id, today);
                                handler.SetLastInterestPaymentDate(deposit.Id, today);
                            }
                        }
                        else if (PaymentPeriodlTimeHasPassed(deposit.StartDate, deposit.LastInterestPaymentDate))
                        {
                            logger.WriteLine($"Started adding interest");

                            handler.AddInterest(deposit.Id);
                            handler.SetLastInterestPaymentDate(deposit.Id, today);

                            if (today.Date == deposit.EndDate.Date)
                            {
                                handler.SetDepositState(deposit.Id, "Pending");
                            }
                        }

                        logger.WriteLine($"Finished processing deposit Id='{deposit.Id}'");
                    }
                    
                } 
                
                logger.WriteLine("Interest payment is completed.");
            }
            catch (Exception e)
            {
                logger.WriteLine("EXCEPTION: " + e.Message + " " + (e.InnerException.Message ?? ""));
                Stop();
                throw;
            }
        } 
        
        // Check if payment interval time has passed since the last interest payment
        private bool PaymentPeriodlTimeHasPassed(DateTime startDate, DateTime? lastPaymentDate)
        {
            logger.WriteLine($"Checking if processing payment interval time has passed since the last interest payment");
            period = double.Parse(configuration["PaymentPeriodDays"]);
            return DateTime.Today >= (lastPaymentDate ?? startDate).AddDays(period);
        }

        private void SaveScheduledDateTime(DateTime nextScheduledDateTime)
        {
            logger.WriteLine($"Saving new 'ScheduledDateTime' value to the app settings file...");
            var configuration = JObject.Parse(File.ReadAllText("appsettings.json"));
            configuration["ScheduledDateTime"] = nextScheduledDateTime.ToString("yyyy-MM-dd HH:mm");
            File.WriteAllText(appSettingsFilePath, configuration.ToString());
            logger.WriteLine($"Finished saving new 'ScheduledDateTime' value to the app settings file...");
        }
    }
}
