using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Console;

namespace ThreadPlayground
{
    class ReaderWriterLockSlimSample
    {
        private static readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();// Usado quando se tem muito mais leitura que escrita
        internal static void Start()
        {
            ParameterizedThreadStart onThread = item =>
            {
                int i = (int)item;

                if (i % 3 == 0)
                {
                    Thread.Sleep(i);
                    //WriteLine("writing news");
                    WriteToLock($"writing news for {i}");
                }
                else
                {
                    Thread.Sleep(i);

                    List<string> newsItems = ReadNews(i);
                    //WriteLine(string.Join(',', newsItems));
                }
            };

            for (int i = 0; i < 10; i++)
            {
                new Thread(onThread).Start(i);
            }
        }

        private static List<string> news = new List<string>();

        private static List<string> ReadNews(int index)
        {
            _rwLock.EnterReadLock();//Try (versão com timeout, melhor)
            WriteLine($"START READ LOCK AT index {index}, currente list info is: {string.Join(',', news)}");

            try
            {
                return news;
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }

        private static void WriteToLock(string nextNews)
        {
            _rwLock.EnterWriteLock();
            WriteLine($"START WRITE LOCK AT writing news: {nextNews}");
            WriteLine($"Total threads waiting for reading: {_rwLock.WaitingReadCount}");

            try
            {
                news.Add(nextNews);
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }
    }
}
