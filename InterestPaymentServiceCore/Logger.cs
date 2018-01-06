using System;
using System.IO;

namespace InterestPaymentService
{
    class Logger
    {
        public string Path { get; set; }

        public Logger(string path)
        {
            Path = path;
        }

        public void WriteLine(string text)
        {
            var textToLog = DateTime.Now + "  " + text + "\r\n";
            Console.Write(textToLog);
            File.AppendAllText(Path, textToLog);
        }
    }
}
