using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pepe_verificaes1
{
    class Program
    {
        static string txt = "";
        static int numB = 0;

        
        static SemaphoreSlim s1 = new SemaphoreSlim(1);
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => Metodo1());
            t1.Start();
            Thread t2 = new Thread(() => Metodo2());          
            t2.Start();
            while (t1.IsAlive) { }
            while (t2.IsAlive) { }
            Console.WriteLine($"{txt.Length} , {numB}");
            Console.ReadLine();
        }
        
        public static void Metodo1()
        {
            char a = 'a';
           
            for (int i = 0; i < 100; i++)
            {
                txt = a + txt;
            }
           
        }

        public static void Metodo2()
        {
            char b = 'b';
            s1.Wait();
            for (int i = 0; i < 100; i++)
            {
                if (txt != "")
                {
                    txt.Substring(0, txt.Length -1);
                }
                else if (txt != "" || txt[i] == b)
                {
                    txt = txt + b;
                    numB++;
                }
            }
            s1.Release();
        }
    }
}
