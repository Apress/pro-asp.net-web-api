using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCompletionSourceIntro {

    class Program {

        static void Main(string[] args) {

            AsyncFactory.GetIntAsync().ContinueWith((task) => {

                if (task.Status == TaskStatus.RanToCompletion) {

                    Console.WriteLine(task.Result);
                }
                else if (task.Status == TaskStatus.Canceled) {

                    Console.WriteLine("The task has been canceled.");
                }
                else {

                    Console.WriteLine("An error has been occurred. Details:");
                    Console.WriteLine(task.Exception.InnerException.Message);
                }
            });

            Console.ReadLine();
        }
    }

    public class AsyncFactory {

        public static Task<int> GetIntAsync() {
            
            var tcs = new TaskCompletionSource<int>();

            var timer = new System.Timers.Timer(2000);
            timer.AutoReset = false;
            timer.Elapsed += (s, e) => {
                tcs.SetResult(10);
                timer.Dispose();
            };

            timer.Start();
            return tcs.Task;
        }
    }
}