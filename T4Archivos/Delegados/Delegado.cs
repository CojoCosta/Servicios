using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegados
{
    public delegate int Operation(int a, int b);
    public class Delegado
    {
        static int Addition(int a, int b)
        {
            return a + b;
        }
        static int Substraction(int a, int b)
        {
            return a - b;
        }
        static void Main(string[] args)
        {
            Operation op1 = (n1, n2) => n1 - n2;
            Operation op2 = (n1, n2) =>
            {
                int res = n1 - n2;
                return res;
            };
            Operation op = new Operation(Substraction); // Operation op = Substraction
            string res;
            int n1, n2;
            Console.WriteLine("Do you want to add or substract?(A/S)");
            res = Console.ReadLine().Trim().ToUpper();
            if (res == "A")
            {
                op = new Operation(Addition); // Operation op = Addition;
            }
            Console.WriteLine("Input first operand");
            n1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input second operand");
            n2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Result: {0}", op(n1, n2));
            Console.ReadKey();
        }
    }
}
