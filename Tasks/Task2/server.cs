using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    static class Server
    {
        private static int count = 0;
        private static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        public static int GetCount()
        {
            locker.EnterReadLock();
            try
            {
                return count;
            }
            finally
            {
                locker.ExitReadLock();
            }
        }
        public static void AddToCount(int value)
        {
            locker.EnterWriteLock();

            Thread.Sleep(4000);// таймер, который для наглядности показывает ожидание читателей, пока происходит процесс записи в переменную.
            //Запись происходит по очереди, поэтому весь вывод почти в конце.

            count += value;
            locker.ExitWriteLock();
        }
    }
}
