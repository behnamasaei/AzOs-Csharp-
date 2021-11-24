using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    public class Complex
    {
        public void Add(int a, int b, int c, int d)
        {
            Console.WriteLine($"{a + c} + ({b + d})i");
        }

        public void Minus(int a, int b, int c, int d)
        {
            Console.WriteLine($"{a - c} + ({b - d})i");
        }

        public void Multi(int a, int b, int c, int d)
        {
            Console.WriteLine($"{(a*c)-(b*d)} + ({(b*c)+(a*d)})i");
        }
    }
}
