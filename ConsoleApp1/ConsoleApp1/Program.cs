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

        static void Main(string[] args)
        {
            //Console.WriteLine("Current thread priority: {0}", Thread.CurrentThread.Priority);
            //Console.WriteLine("Running on all cores available");
            //RunThreads();
            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //Console.WriteLine("Running on a single core");
            //Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            //RunThreads();
            Console.WriteLine("主线程运行");
            CancellationTokenSource cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(callback, cts.Token);
            Console.WriteLine("按下回车来取消计数");
            Console.Read();
            cts.Cancel();
            Console.ReadKey();
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

       

        static void RunThreads()
        {
            var sample = new ThreadSample();

            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";
            var threadTwo = new Thread(sample.CountNumbers);
            threadTwo.Name = "ThreadTwo";

            threadOne.Priority = ThreadPriority.Highest;
            threadTwo.Priority = ThreadPriority.Lowest;
            threadOne.Start();
            threadTwo.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();
        }

        class ThreadSample
        {
            private bool _isStopped = false;
            public void Stop()
            {
                _isStopped = true;
            }
            public void CountNumbers()
            {
                long counter = 0;
                while (!_isStopped)
                {
                    ++counter;
                }
                Console.WriteLine("{0} with {1,11} priority has a count = {2, 13}", Thread.CurrentThread.Name, Thread.CurrentThread.Priority, counter.ToString("NO"));
            }
        }

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
