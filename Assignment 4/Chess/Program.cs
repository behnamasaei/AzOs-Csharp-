using System;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter m:");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n:");
            int n = Convert.ToInt32(Console.ReadLine());
            int counter = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (counter % 2 == 0)
                    {
                        Console.Write("#");
                        counter++;

                    }
                    else
                    {
                        Console.Write("*");
                        counter++;

                    }
                }
                Console.WriteLine();
            }
        }
    }
}
