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
                    Console.WriteLine("① 딜러와 플레이어 모두에게 먼저 2 장씩 카드를 받습니다.\n② 자신이 갖고 있는 카드의 합이 21에 가까워 지도록 카드를 히트(Hit | 추가)하거나 스탠드(Stand | 그대로) 선택할 수 있습니다.\n③ 카드의 합이 21을 넘어 버린 시점(버스트(Bust))에서 패배가 결정됩니다.\n④ 플레이어는 21을 초과하지 않는 한 원하는만큼 카드를 추가 할 수 있습니다.\n⑤ 딜러는 카드의 합이 17을 초과 할 때까지 카드를 추가합니다.");
                    Console.WriteLine();
                    Console.WriteLine("21이 되지 않는 한 얼마든지 원하는 만큼 카드를 뽑을 수 있습니다. 반면 카드의 수 합산이 21이 넘으면 그 즉시 지게됩니다.");
                    Console.WriteLine("\n게임을 시작 하려면 아무키나 입력해주세요....");
                    Console.ReadKey(true);                      // 아무 키나 입력받으면 진행
                    Game game = new Game();
                    game.do_Bet();

                }
                else if (check == "N")
                {
                    Console.WriteLine("\n게임을 시작 하려면 아무키나 입력해주세요....");
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
        //static void ReGame(int nowcoin)
        //{
        //    Console.Clear();
        //    Console.WriteLine("게임을 시작하려면 아무키나 입력해주세요....");
        //    Console.ReadKey(true);

        //}

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
