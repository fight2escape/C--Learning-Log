using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Threading;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Program");
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            Console.WriteLine("A thread has been aborted");
            Thread t2 = new Thread(PrintNumbers);
            t2.Start();
            PrintNumbers();
        }

        static void PrintNumbersWithDelay()
        {
            Console.WriteLine("Starting With Delay");
            for(int i=0; i<10; ++i)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }

        static void PrintNumbers()
        {
            Console.WriteLine("Starting.......");
            for(int i=0; i<10; ++i)
            {
                Console.WriteLine(i); 
            }
        }
    }
}
