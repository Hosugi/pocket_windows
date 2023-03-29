using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ChatPro_client1_클라이언트_간의_메세지_전송_
{
    class ConsoleClient
    {
        // 클라이언트 객체를 담을 멤버
        TcpClient client = null;
        // 스레드로 실행시킬 함수를 담을 스레드 객체
        Thread reciveMsgThread = null;
        // 다중 스레드에 안전한 string 리스트, 보낸 메세지. 받은 메세지를 각 리스트에 저장하는 멤버
        ConcurrentBag<string> sendMsgListView = null;
        ConcurrentBag<string> reciverMsgListView = null;

        // 각 클라이언트의 객체에 담긴 전송자의 닉네임
        private string name = null;

        // 서버구동
        public void Run()
        {
            sendMsgListView = new ConcurrentBag<string>();
            reciverMsgListView = new ConcurrentBag<string>();
            // 스레드에 reciveMessage함수를 매개변수로 넣어준다.
            reciveMsgThread = new Thread(ReciveMessage);

            // 클라이언트 뷰 반복
            while (true)
            {
                Console.WriteLine("=======클라이언트1=======");
                Console.WriteLine($"닉네임: {name}");
                Console.WriteLine("1. 서버 연결하기");
                Console.WriteLine("2. 메세지 보내기 (수신자 지정)");
                Console.WriteLine("3. 보낸 메세지 확인하기");
                Console.WriteLine("4. 받은 메세지 확인하기");
                Console.WriteLine("0. 종료하기");
                Console.WriteLine("========================");

                string ans = Console.ReadLine();
                int key = 0;

                if(int.TryParse(ans, out key))
                {
                    switch (key)
                    {
                        // input 1
                        case StaticDef_client.CONNECT:
                            if(client != null)
                            {
                                Console.WriteLine("이미 연결 되어 있습니다. 메인화면으로 돌아갑니다.");
                                Thread.Sleep(2000);
                                break;
                            }
                            ServerConnect();
                            break;
                        // input 2
                        case StaticDef_client.SEND_MESSAGE:
                            if(client == null)
                            {
                                Console.WriteLine("먼저 서버와 연결 해 주세요.");
                                Thread.Sleep(2000);
                                break;
                            }
                            SendMessage();
                            break;
                        // input 3
                        case StaticDef_client.SEND_MSG_VIEW:
                            SendMessageView();
                            break;
                        // input 4
                        case StaticDef_client.RECIVE_MSG_VIEW:
                            ReciveMessageView();
                            break;
                        // input 0
                        case StaticDef_client.EXIT:
                            if(client != null)
                            {
                                client.Close();
                                break;
                            }
                            // thread 종료 구문
                            reciveMsgThread.Abort();
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("잘못 입력 하셨습니다.");
                }
                Thread.Sleep(1000);
                Console.Clear();
            }

        }

        private void ServerConnect()
        {
            string parsingName = null;

            Console.Write("닉네임을 입력해 주세요: ");
            this.name = Console.ReadLine();

            while (parsingName == null || parsingName == "&^%")
            {
                // 나중에 검사할 CheckID에서 포함되어야 하는 문자열을 추가
                parsingName = "&^%" + name;
                if (parsingName == "&^%")
                {
                    Console.Write("닉네임을 1글자 이상 입력해 주세요: ");
                    this.name = Console.ReadLine();
                    parsingName = "&^%" + name;
                }
            }

            client = new TcpClient();
            // 다른 pc에서 이 프로그램을 이용 하려면 이 구문에 실제 ip와 포트를 입력 해줘야한다.
            client.Connect("127.0.0.1", 9999);
            byte[] nickdata = new byte[1024];
            nickdata = Encoding.Default.GetBytes(parsingName);
            // 클라이언트의 스트림에 전송
            client.GetStream().Write(nickdata, 0, nickdata.Length);

            // 서버에 접속, 메세지를 받아주는 스레드 작동
            try
            {
                reciveMsgThread.Start();
                Console.WriteLine("서버 연결 성공.");
                Thread.Sleep(1000);
            }
            catch(SocketException e)
            {
                Console.WriteLine($"연결에 오류가 발생 했습니다. 에러코드: {e.ErrorCode}");
                Console.WriteLine("메인 화면으로 돌아갑니다.");
                Thread.Sleep(2000);
                return;
            }
            

        }
        
        // 유저리스트에서 상대를 선택해 메세지를 입력받아 byte로 변환 해 전송하는 메서드
        private void SendMessage()
        {
            // 대화할 상대를 선택하는 변수
            string getuserlist = $"{name}<GiveMeUserList>";
            byte[] getuserbyte = Encoding.Default.GetBytes(getuserlist);
            client.GetStream().Write(getuserbyte, 0, getuserbyte.Length);

            Console.Write("메세지를 받을 닉네임을 입력해 주세요: ");
            // 메세지를 받을 상대 닉네임을 보관하는 변수
            string reciver = Console.ReadLine();

            Console.Write("\n메세지를 입력해 주세요: ");
            string msg = Console.ReadLine();

            // 아무것도 입력되지 않았을 때
            if (string.IsNullOrEmpty(reciver) || string.IsNullOrEmpty(msg))
            {
                Console.WriteLine("제대로 입력 되지 않았습니다. 다시 진행해 주세요.");
                Thread.Sleep(2000);
                return;
            }

            string parsingMsg = $"{reciver}<{msg}>";
            byte[] parsingByte = new byte[1024];
            parsingByte = Encoding.Default.GetBytes(parsingMsg);
            client.GetStream().Write(parsingByte, 0, parsingByte.Length);

            sendMsgListView.Add($"{DateTime.Now.ToString("[yy.MM.dd.] HH:mm:ss")} [{name}]: {parsingMsg}");

        }

        private void ReciveMessage()
        {
            string reciveMsg = "";
            // 받는 메세지를 담을 리스트 생성
            List<string> reciveMegList = new List<string>();

            while (true)
            {
                // 클라이언트에서 메세지를 받은 byte형 데이터를 담을 배열 생성
                byte[] reciveByte = new byte[1024];
                // 클라이언트에서 스트림을 통해 읽은 후 byte배열에 넣는다.
                client.GetStream().Read(reciveByte, 0, reciveByte.Length);

                // 빈 스트링에 받은 byte데이터를 인코딩하여 대입한다.
                reciveMsg = Encoding.Default.GetString(reciveByte);

                // >는 받은 메세지에서 끝을 나타낸다. "닉네임<메세지내용>"
                // split하여 분할해준다.
                string[] reciveMegArr = reciveMsg.Split('>');

                foreach(var i in reciveMegArr)
                {
                    // 메세지에 '<' 가 포함되어있지 않으면 이상한 데이터 값을 받아온 것 이므로 그냥 넘긴다.
                    if (!i.Contains('<'))
                    {
                        continue;
                    }
                    // 하트비트 메서드로 보내는 관리자메세지는 무시해준다.
                    if (i.Contains("Admin<TEST"))
                    {
                        continue;
                    }
                    // 리스트에 완성된 메세지를 넣는다.
                    reciveMegList.Add(i);
                }
                ParsingReciveMsg(reciveMegList);

                Thread.Sleep(500);
            }

        }

        // 받은 메세지를 닉네임, 메세지로 나누는 메서드
        private void ParsingReciveMsg(List<string> msglist)
        {
            foreach (var i in msglist)
            {
                // sender = 닉네임 msg = 메세지
                string sender = "";
                string msg = "";

                // 만약 < 를 포함하고 있으면
                if (i.Contains('<'))
                {
                    // <를 기준으로 왼쪽 문자열을 자른 뒤
                    string[] splitMsg = i.Split('<');
                    
                    // 보낸사람 닉네임을 sender에
                    sender = splitMsg[0];
                    // 메세지를 msg에 저장한다.
                    msg = splitMsg[1];

                    // sender가 관리자일때 ( 하트비트 스레드 ) 
                    if(sender == "Admin")
                    {
                        string userList = "";
                        // ? 왜 $일까. 1:1 통신떄는 $ 이고 그룹통신때는 다른 기호인걸까
                        string[] splitUser = msg.Split('$');
                        foreach(var j in splitUser)
                        {
                            if (string.IsNullOrEmpty(j))
                            {
                                continue;
                            }
                            userList += j + ", ";
                        }
                        // 뒤에 붙은 ", " 를 제거해준다.
                        userList = userList.Remove(userList.Length - 2);

                        Console.WriteLine($"(현재 접속 인원) -> [{userList}]");
                        msglist.Clear();
                        return;
                    }
                    // 메세지 온 것을 출력
                    Console.WriteLine($"[메세지가 도착했습니다] {sender}: {msg}");
                    // 받은 메세지 목록에 추가하는 구문
                    reciverMsgListView.Add($"{DateTime.Now.ToString("[yy.MM.dd.] [HH:mm:ss]")} {sender}: {msg}");
                }
            }
            // 받은 메세지 리스트(전달받은 데이터)를 초기화한다.
            msglist.Clear();
        }


        private void SendMessageView()
        {
            if(sendMsgListView.Count == 0)
            {
                Console.WriteLine("보낸 메세지가 없습니다. 메인화면으로 돌아갑니다.");
                Thread.Sleep(1000);
                return;
            }
            foreach(var i in sendMsgListView)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("메인 화면으로 돌아가려면 아무키나 눌러주세요.");
            Console.ReadKey();
        }

        private void ReciveMessageView()
        {
            if (reciverMsgListView.Count == 0)
            {
                Console.WriteLine("받은 메세지가 없습니다. 메인화면으로 돌아갑니다.");
                Thread.Sleep(2000);
                return;
            }
            foreach(var i in reciverMsgListView)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("메인 화면으로 돌아가려면 아무키나 눌러주세요.");
            Console.ReadKey();
        }
    }
}
