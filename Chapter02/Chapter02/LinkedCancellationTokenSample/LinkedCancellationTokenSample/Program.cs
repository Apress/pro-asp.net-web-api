using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedCancellationTokenSample {

    class Program {

        static void Main(string[] args) {

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(1000);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            InternalGetIntAsync(cts.Token).ContinueWith((task) => {

                Console.WriteLine("Elapsed time: {0}ms", watch.Elapsed.TotalMilliseconds);
                watch.Stop();

                //We get the response. 
                //Dispose of the CancellationTokenSource
                //so that it is not going to signal.
                cts.Dispose();

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

        static Task<int> InternalGetIntAsync(CancellationToken token) {

            var cts = new CancellationTokenSource(500);
            var linkedTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(cts.Token, token);

            return AsyncFactory.GetIntAsync(linkedTokenSource.Token);
        }
    }

    public class AsyncFactory {

        public static Task<int> GetIntAsync(
            CancellationToken token = default(CancellationToken)) {

            var tcs = new TaskCompletionSource<int>();

            if (token.IsCancellationRequested) {
                tcs.SetCanceled();
                return tcs.Task;
            }

            var timer = new System.Timers.Timer(2000);
            timer.AutoReset = false;
            timer.Elapsed += (s, e) => {
                tcs.TrySetResult(10);
                timer.Dispose();
            };

            if (token.CanBeCanceled) {

                token.Register(() => {
                    tcs.TrySetCanceled();
                    timer.Dispose();
                });
            }

            timer.Start();
            return tcs.Task;
        }
    }
}
