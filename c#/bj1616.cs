using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    internal class bj1616
    {

        
        static void Main(string[] args)
        {
            int n  = int.Parse(Console.ReadLine());
            int count = 0;
            if(n == 0) { Console.WriteLine(count); return; }            //팩토리얼에 0x0은 0

            for (int i = 2; i <= n; i++)
            {
                if (i % 5 == 0)                                         // 5의 배수일 때 0이 하나씩 늘어남
                {
                    count++;
                }
                if (i % 25 == 0)                                        // 25의 배수일 때 0이 하나씩 늘어남
                {
                    count++;
                }
                if (i % 125 == 0)                                       // 125 배수일때.....
                {
                    count++;
                }
            }
            // 솔직히 답 찾아봤음..
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
