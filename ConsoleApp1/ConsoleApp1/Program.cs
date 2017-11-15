using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Threading;
using System.Diagnostics;

namespace ConsoleApp1
{

    class Program
    {

        static int tickets = 100;
        static object gloalObj = new object();
        
        static void Main(string[] args)
        {
            var sampleForeground = new ThreadSample(1);
            var sampleBackground = new ThreadSample(2);
            var threadOne = new Thread(sampleForeground.CountNumbers);
            threadOne.Name = "ForegroundThread";
            var threadTwo = new Thread(sampleBackground.CountNumbers);
            threadTwo.Name = "BackgroundThread";
            threadTwo.IsBackground = true;

            threadOne.Start();
            threadTwo.Start();
        }

        class ThreadSample
        {
            private readonly int _iterations;
            public ThreadSample(int iterations)
            {
                _iterations = iterations;
            }
            public void CountNumbers()
            {
                for (int i = 0; i < _iterations; ++i)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
                }
            }
        }

        private static void SaleTicketThread1()
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(gloalObj);
                    Thread.Sleep(1);
                    if(tickets > 0)
                    {
                        Console.WriteLine("线程1出票：{0}", tickets--);
                    }
                    else
                    {
                        break;
                    }
                }
                finally
                {
                    Monitor.Exit(gloalObj);
                }
            }
        }

        private static void SaleTicketThread2()
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(gloalObj);
                    Thread.Sleep(1);
                    if(tickets > 0)
                    {
                        Console.WriteLine("线程2出票：{0}", tickets--);
                    }
                    else
                    {
                        break;
                    }
                }
                finally
                {
                    Monitor.Exit(gloalObj);
                }
            }
        }

        private static void callback(object state)
        {
            CancellationToken token = (CancellationToken)state;
            Console.WriteLine("开始计数");
            Count(token, 1000);
        }

        private static void Count(CancellationToken token, int countto)
        {
            for(int i=0; i<countto; ++i)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("计数取消");
                    return;
                }
                else
                {
                    Console.WriteLine("计数为：{0}", i);
                    Thread.Sleep(300);
                }
            }
            Console.WriteLine("计数完成");
        }

        private static void CallBackWorkItem(object state)
        {
            Console.WriteLine("线程池线程开始执行");
            if (state != null)
            {
                Console.WriteLine("线程池线程ID = {0} 传入的参数为 {1}",
                    Thread.CurrentThread.ManagedThreadId, state.ToString());
            }
            else
            {
                Console.WriteLine("线程池线程ID = {0}", Thread.CurrentThread.ManagedThreadId);
            }
        }

       

        //static void RunThreads()
        //{
        //    var sample = new ThreadSample();

        //    var threadOne = new Thread(sample.CountNumbers);
        //    threadOne.Name = "ThreadOne";
        //    var threadTwo = new Thread(sample.CountNumbers);
        //    threadTwo.Name = "ThreadTwo";

        //    threadOne.Priority = ThreadPriority.Highest;
        //    threadTwo.Priority = ThreadPriority.Lowest;
        //    threadOne.Start();
        //    threadTwo.Start();

        //    Thread.Sleep(TimeSpan.FromSeconds(2));
        //    sample.Stop();
        //}

        //class ThreadSample
        //{
        //    private bool _isStopped = false;
        //    public void Stop()
        //    {
        //        _isStopped = true;
        //    }
        //    public void CountNumbers()
        //    {
        //        long counter = 0;
        //        while (!_isStopped)
        //        {
        //            ++counter;
        //        }
        //        Console.WriteLine("{0} with {1,11} priority has a count = {2, 13}", Thread.CurrentThread.Name, Thread.CurrentThread.Priority, counter.ToString("NO"));
        //    }
        //}

        static void DoNothing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        static void PrintNumbersWithStatus()
        {
            Console.WriteLine("Starting With Status");
            Console.WriteLine(Thread.CurrentThread.ThreadState.ToString());
            for (int i = 0; i < 10; ++i)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
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
