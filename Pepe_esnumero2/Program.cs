using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pepe_esnumero2
{
    class Program
    {
        static SemaphoreSlim s1 = new SemaphoreSlim(1);
        static SemaphoreSlim s2 = new SemaphoreSlim(1);

        static int buffer = 0;
        static Random r = new Random();
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => Metti());
            Thread t2 = new Thread(() => Togli());
            t1.Start();
            t2.Start();
            while (t1.IsAlive) { }
            while (t2.IsAlive) { }
        }
        static void Metti()
        {
            int pari = r.Next(1, 100);
            if(pari % 2 == 0)
            {
                if (buffer == 0)
                {
                    buffer = pari;
                    Thread.Sleep(100);
                }
            }
            s2.Release();
            s1.Wait();
        }
        static void Togli()
        {
            if(buffer != 0)
            {
                Console.WriteLine($"usato numero {buffer}");
                buffer = 0;
                Thread.Sleep(50);

            }
            s1.Release();

            s2.Wait();
        }
    }
}
