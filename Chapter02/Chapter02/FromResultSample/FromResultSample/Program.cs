using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromResultSample {

    class Program {

        static void Main(string[] args) {
            
            var intTask = GetIntAsync();

            if (intTask.IsCompleted) {

                Console.WriteLine("Completed Instantly: {0}", intTask.Result);
            }
            else {

                intTask.ContinueWith((task) => {

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
            }

            Console.ReadLine();
        }

        static Task<int> GetIntAsync() {

            return Task.FromResult(10);
        }
    }
}