using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AnonymousDelegates
{
    class Program
    {
        delegate int PointToAddFunc(int num1, int num2);
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            Console.WriteLine("Anonymous");

            PointToAddFunc padd2 = delegate (int n1, int n2) { return n1 + n2; };
            for (int j = 0; j < 10; j++)
            {
                sw.Reset();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    int z = padd2.Invoke(2, 4);
                }
                sw.Stop();
                Console.WriteLine(sw.ElapsedTicks);
            }
            Console.WriteLine("Function CALLING");
            for (int j = 0; j < 10; j++)
            {
                sw.Reset();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    int z = Add(2, 4);
                }
                sw.Stop();
                Console.WriteLine(sw.ElapsedTicks);
            }

            Console.WriteLine("Delegate Pointer");
            PointToAddFunc padd = Program.Add;
            for (int j = 0; j < 10; j++)
            {
                sw.Reset();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    int z = padd.Invoke(2, 4);
                }
                sw.Stop();
                Console.WriteLine(sw.ElapsedTicks);
            }


            Console.ReadLine();
        }

        static int Add(int num1, int num2)
        {

            return num1 *100 + num2;
        }
    }
}
