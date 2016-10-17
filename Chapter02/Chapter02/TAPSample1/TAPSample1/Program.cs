using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAPSample1 {

    class Program {

        static void Main(string[] args) {

            var result = DoWorkAsync().Result;
            Console.WriteLine(result);
            Console.ReadLine();
        }

        static Task<string> DoWorkAsync() {

            return Task<string>.Factory.StartNew(() => {
                Thread.Sleep(3000);
                return "Hello world...";
            });
        }
    }
}
