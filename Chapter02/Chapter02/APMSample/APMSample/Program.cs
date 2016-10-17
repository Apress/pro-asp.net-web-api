using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace APMSample {

    class Program {

        static byte[] buffer = new byte[100];

        static void Main(string[] args) {

            const string filePath = @"C:\Apps\Foo.txt";

            FileStream fileStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read, 1024,
                FileOptions.Asynchronous);

            IAsyncResult result = fileStream.BeginRead(buffer, 0, buffer.Length,
                    new AsyncCallback(CompleteRead), fileStream);

            Console.ReadLine();
        }

        static void CompleteRead(IAsyncResult result) {

            Console.WriteLine("Read Completed");

            FileStream strm = (FileStream)result.AsyncState;

            // Finished, so we can call EndRead and it will return without blocking
            int numBytes = strm.EndRead(result);

            // Don't forget to close the stream
            strm.Close();

            Console.WriteLine("Read {0} Bytes", numBytes);
            Console.WriteLine(BitConverter.ToString(buffer));
        }
    }
}