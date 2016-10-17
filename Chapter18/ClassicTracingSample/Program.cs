using System;
using System.Diagnostics;
using System.Linq;

namespace ClassicTracingSample {
    internal class Program {
        private static string[] _args;

        private static void Main(string[] args) {
            _args = args;
            Trace.Listeners.Add(new ConsoleTraceListener());

            if (tracingEnabled()) {
                Trace.Write("Application started.\r\n");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();

            if (tracingEnabled()) {
                Trace.Write("Application stopped\r\n");
            }
        }

        private static bool tracingEnabled() {
            return _args.ToList().Contains("trace");
        }
    }
}