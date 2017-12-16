using System.Threading;

namespace InterestPaymentService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

#if DEBUG
            var service = new InterestPaymentService();
            service.OnDebug();
            Thread.Sleep(Timeout.Infinite);
#else

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new InterestPaymentService() 
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
