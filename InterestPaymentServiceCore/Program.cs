using System;
using System.Net.Mime;
using System.Threading;

namespace InterestPaymentService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var service = new InterestPaymentService(args);
            Console.ReadLine();
        }
    }
}
