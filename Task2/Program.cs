using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(1, 50, ConnectionToServ);
            Console.WriteLine("финал");
            Console.WriteLine(Server.GetCount());

            Console.ReadKey();
        }
        private static void ConnectionToServ(int n)
        {
            if (n % 2 == 0)
            {
                Console.WriteLine("Получение данных");
                Console.WriteLine(Server.GetCount());
            }
            else
            {
                Console.WriteLine("Изменение переменной");
                Server.AddToCount(n);
            }
        }
    }
}
