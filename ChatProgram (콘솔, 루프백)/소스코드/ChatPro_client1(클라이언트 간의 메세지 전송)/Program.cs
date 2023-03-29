using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPro_client1_클라이언트_간의_메세지_전송_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleClient cc = new ConsoleClient();
            cc.Run();
        }
    }
}
