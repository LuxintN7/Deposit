using System;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using DepositDatabase.Model;

namespace InterestPaymentService
{
    public partial class InterestPaymentService : ServiceBase
    {
        private const int MaxPendingDays = 3;

        private DateTime scheduledDateTime;
        private DateTime currentDateTime;

        private Logger logger;
        private Timer timer;

        private int intervalMinutes;
        private int TimerInterval 
        { 
            get
            { 
                return intervalMinutes * 60 * 1000; 
            } 
        }

        public InterestPaymentService()
        {
            InitializeComponent();
            logger = new Logger(AppDomain.CurrentDomain.BaseDirectory + "log.txt");
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            scheduledDateTime = DateTime.Parse(ConfigurationManager.AppSettings["ScheduledDateTime"]);
            intervalMinutes = Int32.Parse(ConfigurationManager.AppSettings["IntervalMinutes"]);
            
            timer = new Timer(TimerInterval);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();

            logger.WriteLine("Interest Payment Service starts.");
        }

        protected override void OnStop()
        {
            timer.Stop();
            logger.WriteLine("Interest Payment Service stops.");
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            currentDateTime = DateTime.Now;

            if (currentDateTime >= scheduledDateTime
                && currentDateTime < scheduledDateTime.AddMinutes(intervalMinutes))
            {
                RunInterestPayment();
                SaveScheduledDateTime(scheduledDateTime.AddDays(1));
            }
        }

        private void RunInterestPayment()
        {
            logger.WriteLine("Interest payment begins.");

            try
            {
                using (var db = new DepositEntities())
                {
                    var today = DateTime.Now.Date;

                    var deposits = (from d in db.Deposits
                                    where d.DepositStates.Name != "Closed"
                                    select d).ToList();

                    foreach (var deposit in deposits)
                    {
                        if (deposit.DepositStates.Name.Equals("Pending"))
                        {
                            // If a customer does not withdraw a deposit during the pending period 
                            // the deposit will be automatically extended for one more (the same) term
                            if (today - deposit.EndDate >= TimeSpan.FromDays(MaxPendingDays))
                            {
                                deposit.DepositStates = db.DepositStates.First(ds => ds.Name == "Extended");
                                deposit.EndDate = today.AddMonths(deposit.DepositTerms.Months);
                                deposit.LastInterestPaymentDate = today;
                            }
                        }
                        else if (MonthHasPassed(deposit.StartDate, deposit.LastInterestPaymentDate))
                        {
                            AddInterest(db, deposit);
                            deposit.LastInterestPaymentDate = today;

                            if (today.Date == deposit.EndDate.Date)
                            {
                                deposit.DepositStates = db.DepositStates.First(ds => ds.Name == "Pending");
                            }
                        }
                    }
                                                                             
                    db.SaveChanges();
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
        
        // Check if one month has passed since the last interest payment
        private bool MonthHasPassed(DateTime startDate, DateTime? lastPaymentDate)
        {
            return DateTime.Today.Day == (lastPaymentDate ?? startDate).AddMonths(1).Day;
        }

        private void AddInterest(DepositEntities db, Deposits deposit)
        {
            const int daysInYear = 365;
            decimal interestRate = deposit.DepositTerms.InterestRate;
            DateTime today = DateTime.Today;

            byte daysCount = (byte)(today - (deposit.LastInterestPaymentDate ?? deposit.StartDate)).TotalDays;

            decimal interestSum = deposit.Balance * interestRate * daysCount / (100 * daysInYear);

            if (deposit.DepositWaysOfAccumulation.Name == "Capitalization")
            {
                deposit.Balance += interestSum;
            }
            else
            {
                deposit.Cards.Balance += interestSum;
                db.CardHistory.Add(new CardHistory()
                    {
                        DateTime = DateTime.Now,
                        Cards = deposit.Cards,
                        Desription = String.Format("Interest payment in the amount of {0} ({1}).", 
                                                   interestSum, deposit.DepositTerms.Currencies.Name)
                    });
            }
        }

        private void SaveScheduledDateTime(DateTime nextScheduledDateTime)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ScheduledDateTime"].Value = nextScheduledDateTime.ToString("yyyy-MM-dd HH:mm");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
