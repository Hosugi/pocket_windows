using blackjeck;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blackjeck
{   internal class Program
    {
        public static int flag = 0;
        public static int flag2 = 0;
        static void Logo()
        {
            string[,] main = new string[,]
               {{"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□■□□□□□□■□□□■■■■□■□□■□□□■■■■■□■□■□□" },
                {"□□■■■■■■■■□□□□□□■□■□□■□□□□□■□□□■□■□□" },
                {"□□■□□□□□□■□□□■■■■□■■■■□□□□□■□□□■■■□□" },
                {"□□■■■■■■■■□□□■□□□□■□□■□□□□■□■□□■□■□□" },
                {"□■■■■■■■■■■□□■□□□□■□□■□□□□■□■□□■□■□□" },
                {"□□□□□□□□□□□□□■■■■□■□□■□□□■□□□■□■□■□□" },
                {"□□■■■■■■■■□□□□□□□□□□□□□□□□□□□□□□□□□□" },
                {"□□□□□□□□□■□□□□■■■■■■■■□□□□■■■■■■■■□□" },
                {"□□■■■■■■■■□□□□□□□□□□□■□□□□□□□□□□□■□□" },
                {"□□■□□□□□□□□□□□□□□□□□□■□□□□□□□□□□□■□□" },
                {"□□■■■■■■■■□□□□□□□□□□□■□□□□□□□□□□□■□□" },
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□" }
                };
            string[,] main2 = new string[,]
                {{"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"},
                {"□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"}
                };
            int t = 3;
            while (t > 0) {
                Console.Clear();
                foreach (string i in main2) Console.WriteLine(i);
                Thread.Sleep(500);
                Console.Clear();
                foreach (string j in main) Console.WriteLine(j);
                Thread.Sleep(500);
                t--;
            }
            

        }

        static void Rule()
        {
            if (flag == 1)
            {
                Coin coin = new Coin();
                if (coin.GameOver_coin(Coin.P_coin)) {
                    Console.WriteLine("파산하셨습니다.... 새로운 게임을 시작하시려면 re를, 종료하시려면 exit를 입력 해주세요.");
                    string ans = Console.ReadLine();
                    ans = ans.ToUpper();
                    switch (ans) {
                        case "RE":
                            Game game = new Game();
                            game.do_Bet();
                            break;
                        case "EXIT":
                            Gameover();
                            break;
                    }
                }

                else { Game game = new Game(Coin.P_coin); }
            }
            else
            {
                Console.WriteLine("블랙잭의 규칙을 보시겠습니까? Y/N");
                string check = Console.ReadLine();
                ClearCurrentLine();
                check = check.ToUpper();


                if (check == "Y")
                {
                    Console.Clear();
                    RuleSay();
                    Console.WriteLine("\n시작 하려면 아무키나 입력해주세요....");
                    Console.ReadKey(true);                      // 아무 키나 입력받으면 진행
                    Game game = new Game();
                    game.do_Bet();

                }
                else if (check == "N")
                {
                    Console.WriteLine("\n시작 하려면 아무키나 입력해주세요....");
                    Console.ReadKey(true);
                    Game game = new Game();
                    game.do_Bet();
                }
                else                                            // y/n말고 댜른 값 들어올때
                {
                    Console.Clear();
                    Rule();
                }
            }

        }
        public static void RuleSay()
        {
            Console.WriteLine("① 딜러와 플레이어 모두에게 먼저 2 장씩 카드를 받습니다.\n");
            Thread.Sleep(1000);
            Console.WriteLine("② 자신이 갖고 있는 카드의 합이 21에 가까워 지도록 카드를 히트(Hit | 추가)하거나 스탠드(Stand | 그대로) 선택할 수 있습니다.");
            Thread.Sleep(1000);
            Console.WriteLine("③ 카드의 합이 21을 넘어 버린 시점(버스트(Bust))에서 패배가 결정됩니다.\n");
            Thread.Sleep(1000);
            Console.WriteLine("④ 플레이어는 21을 초과하지 않는 한 원하는만큼 카드를 추가 할 수 있습니다. 반면 카드의 수 합산이 21이 넘으면 그 즉시 지게됩니다.\n");
            Thread.Sleep(1000);
            Console.WriteLine("⑤ 딜러는 카드를 공개후 합이 17을 넘지 않을경우 넘을 때까지 카드를 무조건 추가합니다.");
            Thread.Sleep(1000);
            Console.WriteLine("\n⑥ 본 게임에서는 서렌더의 선택지는 없으며 스플릿 또한 불가합니다.");
            Thread.Sleep(1000);
            Console.WriteLine("\n⑦ 플레이어가 두개의 카드로 블랙잭을 성공 할 시에는 딜러의 카드와는 상관없이 무조건 승리하게 되며 딜러가 두개의 카드로 블랙잭을 성공하고, 플레이어가 블랙잭을 성공 못한다면 플레이어의 패배입니다.");
            Thread.Sleep(1000);
            Console.WriteLine("\n⑧ A(에이스)카드는 21을 넘지 않으면 11로 간주하고 21을 넘었을 때 A를 가지고 있다면 A는 1로 간주하여 계산됩니다. \nex) J, 5, A = 16 (변환전 값: 26)");
            Thread.Sleep(1000);
            Console.WriteLine("\n잘 숙지 하셨나요?....");
        }

        static void Start()
        {
            Logo();
            Console.WriteLine("로딩 완료!\n");
            Console.WriteLine("시작하려면 start를 입력해주세요.");
            string go = Console.ReadLine();
            // 대문자 변경
            go = go.ToUpper();

            switch (go)
            {
                case "START":
                    Console.Clear();
                    Rule();
                    break;

                default:
                    Console.Clear();
                    Start();
                    break;
            }
        }
        static void ClearCurrentLine()
        {
            string s = "\r";
            s += new string(' ', Console.CursorLeft);
            s += "\r";
            Console.Write(s);
        }
        
        static public void Gameover()
        {
            Console.Write("\n\n플레이 해 주셔서 감사합니다.\nMade by Hosugi");
            string a = ".";
            for (int i = 0; i < 3; i++)
            {
                Console.Write(a);
                Thread.Sleep(1000);
            }
            Console.WriteLine();
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            Start();
            while (flag == 1) {
                Console.Clear();
                Rule(); 
            }

            Gameover();

        }


    }
}
