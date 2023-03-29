using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Net;
using System.Net.Sockets;

namespace ChatPro_Server
{
    // https://frozenpond.tistory.com/31 참고 - 콘솔 채팅 서버 만들기 
    class ClientManager
    {
        // 여러 스레드에 안전한 Dictionary, key = ip주소의 마지막 8비트(11111111) value = 클라이언트 데이터 객체(접속 유저 정보, 채팅 기록 등등)
        // 접속 유저의 정보들을 담고있는 딕셔너리.
        public static ConcurrentDictionary<int, ClientData> clientDic = new ConcurrentDictionary<int, ClientData>();

        // key는 sender, value는 message.
        public event Action<string, string> messageParsingAction = null;
        // key는 message, value는 key값.
        public event Action<string, int> EventHander = null;

        public void AddClient(TcpClient cl)
        {
            ClientData currentcl = new ClientData(cl);
            try
            {
                // 클라이언트를 set 한 뒤
                currentcl.tcpClient.GetStream().BeginRead(currentcl.readbuffer, 0, currentcl.readbuffer.Length, new AsyncCallback(DataRecived), currentcl);
                // 클라이언트 제너릭에 추가 하는 구문.
                clientDic.TryAdd(currentcl.clientNum, currentcl);
            }
            catch(Exception e)
            {
                // 아무 작업 안함.
            }
        }

        // 데이터를 받는 수신자에게 전달하는 메서드
        private void DataRecived(IAsyncResult ar)
        {
            // 비동기로 받은 객체를 client data로 형변환 한다. 이 객체는 각 클라이언트(유저)의 정보, 채팅등을 담고있다.
            ClientData cd = ar.AsyncState as ClientData;
            try
            {
                // EndRead() 메서드가 어떤 작업을 하는지.? 
                int bytelen = cd.tcpClient.GetStream().EndRead(ar);
                string strData = Encoding.Default.GetString(cd.readbuffer, 0, bytelen);

                cd.tcpClient.GetStream().BeginRead(cd.readbuffer, 0, cd.readbuffer.Length, new AsyncCallback(DataRecived), cd);

                if (string.IsNullOrEmpty(cd.clientName))
                {
                    if (EventHander != null)
                    {
                        if (CheckID(strData))
                        {
                            // &^%을 제외한 인덱스 012<name>
                            string username = strData.Substring(3);
                            cd.clientName = username;
                            string accessLog = $"[{DateTime.Now.ToString("yy.MM.dd HH:mm:ss")}] [{cd.clientName}] 접속";
                            // 이벤트핸들러(가칭). 첫번째 매개변수는 접속기록을 담은 스트링, 두번째 매개변수는 클라이언트에 전달할 key값
                            EventHander.Invoke(accessLog, StaticDefine.ADD_ACCESS_LOG);
                            return;
                        }
                        
                    }
                }
                // mpa 가 null이 아닐 때 (nullable 문법)
                messageParsingAction?.BeginInvoke(cd.clientName, strData, null, null);


            }
            catch (Exception e)
            {
                // 아무 작업 하지않음
                
            }
        }

        // 처음 실행될 때 아이디를 정하는 메서드. 특정 문자열이 포함 되어있으면 제일 처음 등록 하는 유저가 됨.
        private bool CheckID(string str)
        {
            if (str.Contains("&^%")) return true;
            return false;
        }
    }



    public class MainServer
    {
        ClientManager clientManager = null;

        // 병렬처리를 위한 리스트 (채팅기록, 접속기록을 담음)
        ConcurrentBag<string> chatLog = null;
        ConcurrentBag<string> accessLog = null;
        // 현재 연결된 스레드를 체크하는 객체?
        Thread connectCheckThread = null;

        public MainServer()
        {
            // 클라이언트 매니저의 객체를 생성
            // 채팅로그와 접속기록을 담을 컬렉션 생성
            // 서버스레드 시작, 상시로 연결되어 있는지 확인하는 스레드 또한 시작
            clientManager = new ClientManager();
            chatLog = new ConcurrentBag<string>();
            accessLog = new ConcurrentBag<string>();

            clientManager.EventHander += ClientEvent;
            clientManager.messageParsingAction += MessageParsing;

            // 서버스레드 시작. 화살표 함수를 통해 Task.Run() 매개변수로 ServerRun()의 리턴된 것을 전달
            Task serverStart = Task.Run(() => 
            {
                ServerRun();
            });

            // 상시 커넥팅 체크 스레드 시작
            connectCheckThread = new Thread(ConnectCheckLoop);
            connectCheckThread.Start();
        }

        // 하트비트 스레드(클라이언트들의 접속이 연결되어있는지를 1초마다 반복하는 메서드)
        private void ConnectCheckLoop()
        {
            while (true)
            {
                // item에 cliendDic의 value인 ClientData객체를 대입
                foreach (var item in ClientManager.clientDic)
                {
                    try
                    {
                        string sendStringData = "Admin<TEST>";
                        byte[] sendByteData = new byte[sendStringData.Length];
                        // 문자열을 byte로 인코딩 한 뒤 대입
                        sendByteData = Encoding.Default.GetBytes(sendStringData);

                        item.Value.tcpClient.GetStream().Write(sendByteData, 0, sendByteData.Length);
                    }
                    catch (Exception e)
                    {
                        // 연결이 끊어지면 오류가 발생한다.  클라이언트 종료를 서버에 알리고 클라이언트 객체를 제거한다.
                        // value = 클라이언트 객체. (key는 클라이언트 순서)
                        RemoveClient(item.Value);
                    }
                }
                // 1초에 한번 씩 체킹 반복
                Thread.Sleep(1000);
            }
        }

        // 클라이언트 종료가 감지되었을 때 클라이언트 제거, 접속로그 저장하는 메서드
        private void RemoveClient(ClientData cd)
        {
            ClientData result = null;
            // TryRemove()로 key값을 받아 찾아간 뒤 객체를 제거하고 null값을 가진 객체로 채워넣는다.
            ClientManager.clientDic.TryRemove(cd.clientNum, out result);
            // 접속 기록에 담을 기록 문자열 저장 (datetime에서 지원하는 Tostring() 구문)
            string leaveLog = $"{DateTime.Now.ToString("yy년MM월dd일 HH:mm:ss")} {result.clientName} {"leave server"}";
            // 여러 스레드로 부터 보호받는 컬렉션에 값을 추가하려면 Add()함수로 추가해야함.
            accessLog.Add(leaveLog);
        }

        // 메세지 분할 함수(?)
        // 클라이언트에게 메세지를 보내는 첫번째 과정 sender는 보낸사람 이름
        private void MessageParsing(string sender, string message)
        {
            // 메세지가 담길 제네릭 리스트
            List<string> list = new List<string>();
            // '>'를 기준으로 전달받은 메세지를 잘라 보관한다. (버퍼에 쌓인 메세지를 비교하기 위해)
            // 이렇게 구별하지 않는다면 여러메세지가 동시에 입력될 경우 
            string[] splitmsg = message.Split('>');
            // 제너릭 list에 
            foreach (var i in splitmsg)
            {
                // 메세지를 잘라 보관한 배열 인덱스에 아무 값도 없을 때 
                if (string.IsNullOrEmpty(i)) continue;
                // 제너릭 리스트에 한글자씩 추가한다.
                list.Add(i);
            }
            // 
            SendMessageToCt(list, sender);

        }

        // 클라이언트에게 메세지를 보내는 두번째 과정, list는 메세지를 split한 편집본을 담은 리스트
        private void SendMessageToCt(List<string> list, string sender)
        {
            // 채팅 기록을 담을 문자열
            string log = "";
            // 분할 된 문자열을 담음
            string parsed = "";
            // 리시버
            string reciver = "";

            // 발신자 번호
            int senderNum = -1;
            // 수신자 번호
            int reciverNum = -1;

            foreach (var i in list)
            {
                // '<'를 기준으로 전달받은 메세지를 잘라 보관한다.
                string[] splitmsg = i.Split('<');
                // 잘라놨던 문자열을 리시버에 대입
                reciver = splitmsg[0];
                // 보낸사람 이름과 메세지를 분할한 것, 서버에 전달할 메세지의 모든 것
                parsed = string.Format("{0}<{1}>", sender, splitmsg[1]);

                // 발신, 수신자의 번호를 받아옴
                senderNum = GetClientNum(sender);
                reciverNum = GetClientNum(reciver);

                // 만약 받아온 사용자의 이름이 없는 경우
                if (senderNum == -1 || reciverNum == -1)
                {
                    return;
                }

                if (parsed.Contains("<GiveMeUserList>"))
                {
                    // 관리자는 항상 존재하므로
                    string userListdata = "Admin<";
                    // 각 인스턴스의 딕셔너리가 아닌 정적 인스턴스의 딕셔너리(사용자 정보)를 불러옴
                    foreach (var j in ClientManager.clientDic)
                    {
                        // 유저이름을 한글자씩 data에 더해줌
                        userListdata += string.Format($"{j.Value.clientName}$");
                        //userListdata += " ";
                    }
                    userListdata += ">";
                    // byte로 변환 후 서버로 전송하는 과정들, 유저 리스트들을 담은 문자열 길이만큼의 byte 배열을 만듬.
                    byte[] userListBytedata = new byte[userListdata.Length];
                    // 인코딩 후 저장
                    userListBytedata = Encoding.Default.GetBytes(userListdata);
                    // 정적으로 저장되어있는 딕셔너리의 키값: 수신자의 번호의 클라이언트에 전송. byte 배열, 읽기 시작할 위치, 총 길이를 전송
                    ClientManager.clientDic[reciverNum].tcpClient.GetStream().Write(userListBytedata, 0, userListBytedata.Length);
                    return;
                }
                // 일반적인 메세지 전송

                // 채팅 기록을 저장하는 구문. splitmsg[1]은 name<content>에서 content만 가져온것.
                log = $"[{DateTime.Now.ToString("[yy.MM.dd HH:mm:ss]")}] [{sender}] -> [{reciver}] : {splitmsg[1]}";
                // 채팅 로그와 채팅로그를 추가하는 정적맴버인 6을 전달한다.
                ClientEvent(log, StaticDefine.ADD_CHATTING_LOG);
                // 동일하게 전달할 메세지를 byte로 인코딩 해준다.
                byte[] sendbytedata = Encoding.Default.GetBytes(parsed);
                // 정적으로 저장되어있는 딕셔너리의 키값: 수신자의 번호의 클라이언트에 전송. byte 배열, 읽기 시작할 위치, 총 길이를 전송
                ClientManager.clientDic[reciverNum].tcpClient.GetStream().Write(sendbytedata, 0, sendbytedata.Length);
            }
        }

        // 접속한 각 클라이언트 들의 번호를 가져오는 메서드
        private int GetClientNum(string target)
        {
            foreach (var i in ClientManager.clientDic)
            {
                // 만약 저장되어있는 사용자 이름과 받아온 이름이 같다면
                if (i.Value.clientName == target)
                {
                    // 그 사용자의 클라이언트 번호를 반환
                    return i.Value.clientNum;
                }
            }
            // 사용자의 이름이 존재하지 않을때 -1를 반환
            return -1;
        }

        // 접근기록과 채팅기록을 저장하는 메서드
        private void ClientEvent(string message, int key)
        {
            switch (key)
            {
                // 5
                case StaticDefine.ADD_ACCESS_LOG:
                    accessLog.Add(message);
                    break;
                // 6
                case StaticDefine.ADD_CHATTING_LOG:
                    chatLog.Add(message);
                    break;

                default:
                    Console.WriteLine("오류가 발생했습니다.");
                    return;
            }
        }

        // 서버를 돌릴 때 호출 하는 함수
        private void ServerRun()
        {
            // Tcp 클라이언트를 읽는 listener 객체 생성
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Any, 9999));
            listener.Start();

            // 새로운 클라이언트 연결을 계속 받는작업 반복
            while (true)
            {
                // 실시간으로 연결 요청한 것을 작업 대기중인 tcp객체에 연결대기 한다.
                Task<TcpClient> acceptTask = listener.AcceptTcpClientAsync();
                // 기본 값 -1을 전달 함으로서 무한 대기 시킨다(?). (wait()함수에 매개변수를 전달하지 않으면 -1를 전달)
                acceptTask.Wait();
                // acceptTask의 tcpclient 대기 객체를 새로운 tcpclient 객체에 대입한다
                TcpClient newCl = acceptTask.Result;
                // 접속한 클라이언트를 메인 클라이언트 매니저에 추가한다.
                clientManager.AddClient(newCl);

            }
        }
        // 서버에 나타나는 메인 화면
        public void ServerMainView()
        {
            while (true)
            {
                Console.WriteLine("===========서버============");
                Console.WriteLine("1. 접속중인 유저 확인");
                Console.WriteLine("2. 접속 기록 확인");
                Console.WriteLine("3. 채팅 로그 확인");
                Console.WriteLine("0. 서버 종료하기");
                Console.WriteLine("===========================");

                string ans = Console.ReadLine();
                int key = 0;

                if (int.TryParse(ans, out key))
                {
                    switch (key)
                    {
                        // 1
                        case StaticDefine.SHOW_CURRENT_CLIENT:
                            ShowCurrentClient();
                            break;
                        // 2
                        case StaticDefine.SHOW_ACCESS_LOG:
                            ShowAccessLog();
                            break;
                        // 3
                        case StaticDefine.SHOW_CHATTING_LOG:
                            ShowChatLog();
                            break;
                        // 0
                        case StaticDefine.EXIT:
                            // 스레드 종료구문
                            connectCheckThread.Abort();
                            return;

                        default:
                            Console.WriteLine("잘못 입력 하셨습니다.");
                            Thread.Sleep(2000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다. 숫자를 정확히 입력해 주세요.");
                    Thread.Sleep(2000);
                }
                Console.Clear();
                Thread.Sleep(100);
            }
        }

        // 현재 접속되어있는 클라이언트(유저)를 보여주는 메서드
        private void ShowCurrentClient()
        {
            if(ClientManager.clientDic.Count == 0)
            {
                Console.WriteLine("현재 접속중인 유저가 없습니다.\n아무키나 누르면 메인으로 돌아갑니다.");
                Console.ReadKey();
                return;
            }
            foreach(var i in ClientManager.clientDic)
            {
                Console.WriteLine($"{i.Value.clientName}");

            }
            Console.WriteLine("\n아무키나 누르면 메인으로 돌아갑니다.");
            Console.ReadKey();
        }

        private void ShowAccessLog()
        {
            if(accessLog.Count == 0) { Console.WriteLine("접속 기록이 없습니다.\n아무키나 누르면 메인으로 돌아갑니다."); Console.ReadKey(); return; }
            foreach (var i in accessLog)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("\n아무키나 누르면 메인으로 돌아갑니다.");
            Console.ReadKey();
        }

        private void ShowChatLog()
        { 
            if (chatLog.Count == 0) { Console.WriteLine("채팅 기록이 없습니다.\n아무키나 누르면 메인으로 돌아갑니다."); Console.ReadKey(); return; }
            
            foreach(var i in chatLog)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("\n아무키나 누르면 메인으로 돌아갑니다.");
            Console.ReadKey();
        }
    }

}
