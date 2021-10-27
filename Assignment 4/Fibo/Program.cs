using System;
using System.Collections.Generic;

namespace Fibo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter n:");
            int n = Convert.ToInt32(Console.ReadLine());

            int[] Fibo = new int[n + 1];

            Fibo[0] = 0;
            Fibo[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                Fibo[i] = Fibo[i - 1] + Fibo[i - 2];
            }

            foreach (var item in Fibo)
            {
                Console.WriteLine(item);
            }
        }
    }
}
