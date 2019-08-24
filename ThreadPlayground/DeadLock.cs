using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ThreadPlayground
{
    internal static class DeadLock
    {
        private static readonly object lock_A = new object();
        private static readonly object lock_B = new object();

        public static void Start()
        {
            //SelectDeadLock("teste").Wait();
            //TimeOut();
            LockWithTimeOutMonitor();
        }

        public static Task SelectDeadLock(string x)
        {
            new Thread(AcquireOne).Start();
            new Thread(AcquireTwo).Start();

            return Task.CompletedTask;
        }

        public static void TimeOut()
        {
            var t1 = new Thread(AcquireOne) { IsBackground = true };
            var t2 = new Thread(AcquireTwo) { IsBackground = true };

            t1.Start();
            t2.Start();

            // Sempre dará timeout, pois a thread está com deadlock
            // Bloqueia a thread principal (que poderia ser liberada para executar outras ações)
            string timeOut2 = t2.Join(TimeSpan.FromSeconds(1)) ? "joined" : "timed out";
            string timeOut1 = t1.Join(TimeSpan.FromSeconds(3)) ? "joined" : "timed out";

            WriteLine($"tried to join threads = 1:{timeOut1}, 2:{timeOut2}");
        }

        public static void LockWithTimeOutMonitor()
        { 
            new Thread(() =>
            {
                try
                {
                    using (lock_A.Lock(2000))
                    {
                        WriteLine("Thread 1, acquired Lock A");
                        using (lock_B.Lock(1000))// *** Ao alternas os valores aqui e no outro trecho com ***, um dos dois entrará 1* (Monitor.Enter), e o outro disparará exceção de timeout (race condition)
                        {
                            WriteLine("Thread 1, acquired Lock B");
                        }
                    }
                }
                catch (TimeoutException)
                {
                    WriteLine("catched exception from thread 1");
                }
            }).Start();

            new Thread(() =>
            {
                try
                {
                    using (lock_B.Lock(1000))
                    {
                        WriteLine("Thread 2, acquired Lock B");
                        using (lock_A.Lock(1000))// *** Ao alternas os valores aqui e no outro trecho com ***, um dos dois entrará 1* (Monitor.Enter), e o outro disparará exceção de timeout (race condition)
                        {
                            WriteLine("Thread 2, acquired Lock A");
                        }
                    }
                }
                catch (TimeoutException)
                {
                    WriteLine("catched exception from thread 2");
                }
            }).Start();
        }

        private static void AcquireOne(object obj)
        {
            lock (lock_A)
            {
                Thread.Sleep(2000);
                WriteLine("Will block here: A");
                lock (lock_B)
                {
                    WriteLine("Never going be reached");
                }
            }
        }

        private static void AcquireTwo(object obj)
        {
            lock (lock_B)
            {
                Thread.Sleep(900);
                WriteLine("Will block here: B");
                lock (lock_A)
                {
                    WriteLine("Never going be reached");
                }
            }
        }


    }
}
