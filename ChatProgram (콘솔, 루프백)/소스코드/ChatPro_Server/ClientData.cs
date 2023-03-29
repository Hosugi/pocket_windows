using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatPro_Server
{
    internal class ClientData
    {
        // 루프백 ip의 4번째 번호를 담을 변수(클라이언트 순서)
        public int clientNum { get; set; }
        // 클라이언트의 이름(사용자이름) 프로퍼티
        public string clientName { get; set; }
        // 각 tcp클라이언트 객체를 담아두는 멤버 프로퍼티
        public TcpClient tcpClient { get; set; }
        // 잘 모르겠으나 버퍼를 보관하는 byte타입의 배열
        public byte[] readbuffer { get; set; }
        public StringBuilder currentMessage { get; set; }

        public ClientData(TcpClient cl)
        {
            // 일반 string으로 하면 성능에 문제가 발생하므로 stringbuilder를 사용하여 저장
            currentMessage = new StringBuilder();
            readbuffer = new byte[1024];
            // 생성자에서 받아온 클라이언트 객체를 cldata의 멤버 클라이언트에 대입
            this.tcpClient = cl;

            char[] splitChar = new char[2];
            splitChar[0] = '.';
            splitChar[1] = ':';

            // 스플릿에 필요한 스트링 배열을 선언
            string[] temp = null;

            // 클라이언트 객체의 ip주소, 포트번호를 (127.0.0.x:9999) 각각 스플릿하여 저장, ':'을 만나기전까지의 모든 값을 저장
            // temp[0] = 127, temp[1] = 0 ....
            temp = tcpClient.Client.LocalEndPoint.ToString().Split(splitChar);

            // 루트백 아이피 127.0.0.x 에서 x를 인스턴스 클라이언트의 번호로 지정해준다.
            this.clientNum = int.Parse(temp[3]);
        }

    }
}
