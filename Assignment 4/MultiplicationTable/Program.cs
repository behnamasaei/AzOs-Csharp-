using System;

namespace MultiplicationTable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter m:");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n:");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    Console.Write(i*j+"\t");
                }
                Console.WriteLine();
            }
        }
    }
}
