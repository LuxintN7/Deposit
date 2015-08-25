using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;
using Microsoft.Practices.Unity;
using DomainLogic;
using DomainLogic.Model;
using DomainLogic.Handlers;
using DepositDatabase.Handlers;

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

        private UnityContainer diContainer;

        public InterestPaymentService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            diContainer = new UnityContainer();
            diContainer.RegisterType<IInterestPaymentHandler, InterestPaymentHandler>(new InjectionConstructor());

            logger = new Logger(AppDomain.CurrentDomain.BaseDirectory + "log.txt");

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
                using (var handler = diContainer.Resolve<IInterestPaymentHandler>())
                {
                    var today = DateTime.Now.Date;

                    var deposits = handler.GetActiveDeposits();

                    foreach (var deposit in deposits)
                    {
                        if (handler.GetDepositStateName(deposit.Id) == "Pending")
                        {
                            // If a customer does not withdraw a deposit during the pending period 
                            // the deposit will be automatically extended for one more (the same) term
                            if (today - deposit.EndDate >= TimeSpan.FromDays(MaxPendingDays))
                            {
                                handler.SetDepositState(deposit.Id, "Extended");
                                handler.ExtendDeposit(deposit.Id, today);
                                handler.SetLastInterestPaymentDate(deposit.Id, today);
                            }
                        }
                        else if (MonthHasPassed(deposit.StartDate, deposit.LastInterestPaymentDate))
                        {
                            handler.AddInterest(deposit.Id);
                            handler.SetLastInterestPaymentDate(deposit.Id, today);

                            if (today.Date == deposit.EndDate.Date)
                            {
                                handler.SetDepositState(deposit.Id, "Pending");
                            }
                        }
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
        
        // Check if one month has passed since the last interest payment
        private bool MonthHasPassed(DateTime startDate, DateTime? lastPaymentDate)
        {
            return DateTime.Today.Day == (lastPaymentDate ?? startDate).AddMonths(1).Day;
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
