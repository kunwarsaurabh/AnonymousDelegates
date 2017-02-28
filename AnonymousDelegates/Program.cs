using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousDelegates
{
    class Program
    {
        delegate int PointToAddFunc(int num1, int num2);
        static void Main(string[] args)
        {
            PointToAddFunc padd = Program.Add;
            Console.WriteLine(padd.Invoke(2,4));
            Console.ReadLine();
        }

        static int Add(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
