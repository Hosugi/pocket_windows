using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace test2
{
    internal class Program
    {
        static int d(int i)
        {
            int total = i;
            while (i != 0)
            {
                total += (i % 10);
                i /= 10;
            }
            return total;
        }
        static void Main(string[] args)
        {
            bool[] ans = new bool[10001];
            for (int i = 1; i < 10001; i++)
            {
                int a = d(i);
                if (a < 10001)
                {
                    ans[a] = true;
                }
            }
            for (int i = 1; i < 10001; i++)
            {
                if (!ans[i])
                {
                    Console.WriteLine(i);
                }
            }

        }
    }

}
