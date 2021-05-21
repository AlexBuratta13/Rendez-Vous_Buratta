using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rendez_Vous_Buratta
{
    class Program
    {
        public static int[] V = new int[1000];
        public static int[] W = new int[1000];

        public static double media = 0;

        public static SemaphoreSlim sem1 = new SemaphoreSlim(0);
        public static SemaphoreSlim sem2 = new SemaphoreSlim(0);

        public static int min = int.MaxValue;
        private static void Main(string[] args)
        {
            Thread t1 = new Thread(() => Metodo1());
            t1.Start();
            Thread t2 = new Thread(() => Metodo2());
            t2.Start();

            while (t1.IsAlive) { }
            while (t2.IsAlive) { }

            Console.WriteLine($"minimo = {min}");
            Console.WriteLine($"media = {media}");

            Console.ReadLine();
        }
        private static void Metodo1()
        {
            Random r = new Random();

            for (int i = 0; i < V.Length; i++)
            {
                V[i] = r.Next(0, 1000);
                if (V[i] < min)
                {
                    min = V[i];
                }
            }
            sem2.Release();

            sem1.Wait();
            for (int i = 0; i < W.Length; i++)
            {
                if (W[i] < min)
                {
                    min = W[i];
                }
            }
        }

        private static void Metodo2()
        {
            Random r = new Random();

            for (int i = 0; i < W.Length; i++)
            {
                W[i] = r.Next(0, 1000);
                media += W[i];
            }
            sem1.Release();

            sem2.Wait();
            for (int i = 0; i < V.Length; i++)
            {
                media += V[i];
                media = media / 2000;
            }
        }
    }
}
