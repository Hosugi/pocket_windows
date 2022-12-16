using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace test2
{
    internal class Program
    {
        class Coordinates : IComparable
        {
            public int x, y;
            public Coordinates(int _x, int _y)
            {
                x = _x;
                y = _y;
            }

            public int CompareTo(object obj)                                    // interface 
            {
                if(obj == null) { return 1; }

                Coordinates that = (Coordinates)obj;
                if (x > that.x) { return 1; }
                else if (x < that.x) { return -1; }
                else
                {
                    if (y > that.y) { return 1; }
                    else { return -1; }
                }
            }
        }
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            Coordinates[] arr = new Coordinates[n];
            for (int k = 0; k < n; k++)
            {
                string[] str = Console.ReadLine().Split();
                arr[k] = new Coordinates(int.Parse(str[0]), int.Parse(str[1]));
            }
            Array.Sort(arr);
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < n; k++) { sb.AppendFormat("{0} {1}\n", arr[k].x, arr[k].y); }
            Console.Write(sb);
        }
    }

}
