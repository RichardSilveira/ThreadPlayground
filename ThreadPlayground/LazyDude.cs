using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using static System.Console;

namespace ThreadPlayground
{
    internal class LazyDude
    {
        public string Nome { get; set; }
        public string AnotherProperty { get; set; }

        public LazyDude()
        {
            WriteLine($"ctor invocado quando uma propriedade for setada, curr thread: {Thread.CurrentThread.Name}");
            Thread.Sleep(2000);
            Nome = "Initial Name";
        }

        public override string ToString() => $"{this.GetType().Name} : {Nome}, {AnotherProperty}";
    }

    internal class RunLazy
    {
        public static void Start()
        {
            var lazy = new Lazy<LazyDude>(true);

            for (int i = 0; i < 10; i++)
            {
                int index = i;
                WriteLine($"starting thread {index}");

                new Thread(() => DoLazy(lazy, $"Thread_{index}"))
                {
                    Name = $"Thread_{index}"
                }.Start();
            }
        }

        private static void DoLazy(Lazy<LazyDude> lazy, string name)
        {
            Thread.Sleep(100);
            WriteLine("did not wait a second for this");

            var timer = new Stopwatch();
            timer.Start();

            lock (_safeGuard)
            {
                lazy.Value.Nome = name;
                lazy.Value.AnotherProperty = name;
            }
            timer.Stop();

            WriteLine($"{lazy} for thread: {name} on elapsed: {timer.Elapsed.TotalSeconds} seconds ");
        }

        private static readonly object _safeGuard = new object();
    }
}
