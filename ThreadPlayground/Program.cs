using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace ThreadPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomTests.Start(args);
            //ThreadBasics.Start(args);

            //ExclusiveLock.Start();
            //NonExclusiveLockSemaphore.Start();
            //ReaderWriterLockSlimSample.Start();

            //RunLazy.Start();
            //ParallelInvokeSimple.Start();
            //ParallelFor.Start();
            //ParallelForEach.Start();
            //DeadLock.Start();

            /*var data = new List<string>()
            {
                "richard",
                "richard",
                "richard",
                "julieth",
                "julieth",
                "floripa"
            };

            data = new List<string>();

            var result = freqCnt(data);*/
        }


        public static Dictionary<T, Int32> freqCnt<T>(IEnumerable<T> data)
        {
            return data.ToList().Aggregate(new Dictionary<T, Int32>(), (acc, item) =>
            {
                if (acc.ContainsKey(item))
                {
                    Int32 value = 0;
                    acc.TryGetValue(item, out value);
                    acc[item] = value + 1;
                }
                else
                    acc.Add(item, 1);

                return acc;
            });
/*

            for (Int32 i = 0; i < data.Count(); i++)
            {
                if (result.ContainsKey(data[i]))
                {
                    Int32 value = 0;
                    result.TryGetValue(data[i], out value);
                    result.Remove(data[i]); // -- otherwise an exception on the next line?!?
                    result.Add(data[i], value + 1);
                }
                else
                    result.Add(data[i], 1);
            }

            return result;*/
        }
    }
}