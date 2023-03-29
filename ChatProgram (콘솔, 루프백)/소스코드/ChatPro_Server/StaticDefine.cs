using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPro_Server
{
    // 정적 맴버를 만들어 놓은 클래스. 하지만 왜 클래스까지 만들어가며 정의하는지는 잘 모르겠다.
    internal class StaticDefine
    {
        // 콘솔에서 switch를 대신하여 구분해놓은 구문
        // 서버, 클라이언트 전부 사용
        public const int SHOW_CURRENT_CLIENT = 1;
        public const int SHOW_ACCESS_LOG = 2;
        public const int SHOW_CHATTING_LOG = 3;

        // 채팅기록이나 접속기록을 추가 하는 값
        // 클라이언트에서만 사용
        public const int ADD_ACCESS_LOG = 5;
        public const int ADD_CHATTING_LOG = 6;

        public const int EXIT = 0;
    }
}
