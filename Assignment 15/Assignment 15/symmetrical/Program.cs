using System;
using System.Collections.Generic;

namespace symmetrical
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input count numbers:");
            int countNumber = Convert.ToInt32(Console.ReadLine());

            List<int> Numbers = new List<int>();

            for (int i = 0; i < countNumber; i++)
            {
                Console.WriteLine($"Enter number {i + 1}");
                Numbers.Add(Convert.ToInt32(Console.ReadLine()));
            }
            bool symmetrical = true;

            for (int i = 0; i < countNumber / 2; i++)
            {
                if (Numbers[i] != Numbers[Numbers.Count - (i + 1)])
                    symmetrical = false;
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine($"Array symmetrical is {symmetrical}");
        }
    }
}
