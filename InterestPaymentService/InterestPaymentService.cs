using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using DepositDatabase.Model;

namespace InterestPaymentService
{
    public partial class InterestPaymentService : ServiceBase
    {
        private readonly string log = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        private const int MaxPendingDays = 3;

        private DateTime scheduledDateTime;
        private DateTime currentDateTime;
        
        private Timer timer;

        private  double intervalMinutes = 0.8;
        private int TimerInterval { get { return (int)(intervalMinutes * 60 * 1000); } }
        //private int TimerInterval = (int)(intervalMinutes * 60 * 1000); 

        public InterestPaymentService()
        {
            InitializeComponent();
        }

        //public void OnDebug()
        //{
        //    OnStart(null);
        //}

        protected override void OnStart(string[] args)
        {
            //scheduledDateTime = DateTime.Parse(ConfigurationManager.AppSettings["ScheduledDateTime"]);
            //intervalMinutes = Double.Parse(ConfigurationManager.AppSettings["IntervalMinutes"]);

            //scheduledDateTime = new DateTime(2015,7,16,0,36,0);
            
            timer = new Timer(TimerInterval);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();

            File.AppendAllText(log, "\r\n" + DateTime.Now + " Interest Payment Service starts.");
        }
        
        protected override void OnStop()
        {
            timer.Stop();
            File.AppendAllText(log, "\r\n" + DateTime.Now + " Interest Payment Service stops.");
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            currentDateTime = DateTime.Now;

            //if (currentDateTime >= scheduledDateTime
            //    && currentDateTime < scheduledDateTime.AddMinutes(intervalMinutes))
            //{
                RunInterestPayment();
            //}

            //scheduledDateTime = scheduledDateTime.AddDays(1);
        }

        private void RunInterestPayment()
        {
            File.AppendAllText(log, "\r\n" + DateTime.Now + "   Run!");

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
                        if (MonthHasPassed(deposit.StartDate, deposit.LastInterestPaymentDate))
                        {
                            if (deposit.DepositStates.Name == "Pending")
                            {
                                // If a customer does not withdraw a deposit during the pending period 
                                // the deposit will be automatically extended for one more (the same) term
                                if (deposit.LastInterestPaymentDate.Value - today >= TimeSpan.FromDays(MaxPendingDays))
                                {
                                    deposit.DepositStates = db.DepositStates.First(ds => ds.Name == "Extended");
                                    deposit.EndDate = today.AddMonths(deposit.DepositTerms.Months);
                                    deposit.LastInterestPaymentDate = today;
                                }
                            }
                            else
                            {
                                AddInterest(db, deposit);
                                deposit.LastInterestPaymentDate = today;

                                if (today.Date == deposit.EndDate.Date)
                                {
                                    deposit.DepositStates = db.DepositStates.First(ds => ds.Name == "Pending");
                                }
                            }
                        }
                    }
                                                                             
                    db.SaveChanges();
                } 
                
                File.AppendAllText(log, "\r\n" + DateTime.Now + "   Completed!");
            }
            catch (Exception e)
            {
                File.AppendAllText(log, "\n\n" + e.Message + (e.InnerException.Message ?? "") + (e.InnerException.Message ?? ""));
                this.Stop();
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
            else if (deposit.DepositWaysOfAccumulation.Name == "Transfer to the card")
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


    }
}
