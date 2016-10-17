using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAPSample2 {

    class Program {

        static void Main(string[] args) {

            DoWorkAsync().ContinueWith(task => {

                Console.WriteLine(task.Result);
            });
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