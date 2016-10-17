using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAPSample4 {

    class Program {

        static void Main(string[] args) {

            var doWorkTask = DoWorkAsync();

            if (doWorkTask.IsCompleted) {

                Console.WriteLine(doWorkTask.Result);

            } else {

                doWorkTask.ContinueWith(task => {

                    Console.WriteLine(task.Result);
                }, TaskContinuationOptions.NotOnFaulted);

                doWorkTask.ContinueWith(task => {

                    Console.WriteLine(task.Exception.Message);
                }, TaskContinuationOptions.OnlyOnFaulted);

                Console.ReadLine();
            }
        }

        static Task<string> DoWorkAsync() {

            return Task<string>.Factory.StartNew(() => {
                
                return "Hello world...";
            });
        }
    }
}
