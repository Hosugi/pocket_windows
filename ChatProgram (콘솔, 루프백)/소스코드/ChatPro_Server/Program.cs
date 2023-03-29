using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChatPro_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //An<int, string> an = new An<int, string>();

            //var i = Console.ReadLine();
            MainServer ms = new MainServer();
            ms.ServerMainView();

        }
    }

    //class An<T1,T2>
    //{
    //    public T1 ID;
    //    public T2[] arr;
    //}





}
