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
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
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
