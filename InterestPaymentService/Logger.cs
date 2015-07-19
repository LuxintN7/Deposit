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
            File.AppendAllText(Path, DateTime.Now + "  " + text + "\r\n");
        }
    }
}
