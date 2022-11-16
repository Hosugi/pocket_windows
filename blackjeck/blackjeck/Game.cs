using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blackjeck
{
    internal class Game : Coin
    {
        public int bet = 0;
        public string s_bet;
        public int p_hit_count = 0;
        public int d_hit_count = 0;
        public int p_total = 0;
        public int d_total = 0;
        public bool win = true;
        public bool p_bj = false;
        public Game()                                                           // 첫 생성자
        {
            P_coin = 100000;                                                    // 첫 게임 시 기본코인 세팅
        }
        public Game(double nowcoin)                                                // RE게임 생성자
        {
            P_coin = nowcoin;
            if(nowcoin == 0) { }
            do_Bet();
        }
        public void do_Bet()
        {
            First:
            Console.Clear();
            Now_coin(P_coin);                                       // 현재 금액 알려주기(배팅전)
            Console.WriteLine("\n\n\n");
            Console.WriteLine("배팅 하실 금액을 입력 해주세요.");

            try
            {
                s_bet = Console.ReadLine();
                bet = Convert.ToInt32(s_bet);
                if(bet == 0) goto First;
            }
            catch
            {
                //do_Bet();                                         함수가 한번더 실행 되어버려서 값이 바뀜
                goto First;
            }

            if (MoreCoin(P_coin, bet) == false)
            {
                goto First;
            }
            else {
                Betting(bet);                                       // 배팅한 값을 플레이어 코인에서 빼기
                Play(); 
            }
        }


        public bool Bj_Check(int total)
        {
            if (total == 21) return true;
            return false;
        }

        public void Player_hit(ref Card card)
        {
            int p_random;
            p_random = card.RandomNum();                                    //카드 위치
            card.p_number[1 + p_hit_count] = card.GetCard(p_random);        //카드 실제숫자
            card.SwapItoP(ref card.p_number[1+p_hit_count],ref card.p_s_number[1+p_hit_count]); // AKQJ 변환
            card.SuitDivide(p_random, ref card.p_patten[1 + p_hit_count]);  // 문양 받아오기
        }

        public void Player_stay(ref Card card)
        {
            Thread.Sleep(1000);
            D_open(ref card);
        }

        public void Dealer_hit(ref Card card)
        {
            int d_random;
            d_random = card.RandomNum();                                    //카드 위치
            card.d_number[1 + d_hit_count] = card.GetCard(d_random);        //카드 실제숫자
            card.SwapItoP(ref card.d_number[1 + d_hit_count], ref card.d_s_number[1 + d_hit_count]); // AKQJ 변환
            card.SuitDivide(d_random, ref card.d_patten[1 + d_hit_count]);  // 문양 받아오기
        }

        public void Total(ref int total, int addcard)                       // 합 계산하기
        {
            total = total + addcard;
        }

        public void P_blacksay()                                            //플레이어 블랙잭 때 출력
        {
            Console.WriteLine("\n블랙잭!!!!!\n\n플레이어가 블랙잭 이므로 배팅 금액의 2.5배를 얻습니다.\n");
            Thread.Sleep(1500);
        }

        public void P_blacksay_hit()
        {
            Console.WriteLine("\n블랙잭!!!!!\n\n");
            Thread.Sleep(1500);
        }

        public void P_bust()                                                // 플레이어 버스트
        {
            Thread.Sleep(1000);
            Console.WriteLine("\n플레이어 카드의 합이 21을 넘었습니다. 배팅한 코인은 사라집니다.\n");
            Thread.Sleep(1000);
            
        }

        public void D_holdSay(ref Card card)                                             // 화면 초반에 고정될 딜러, 플레이어 카드내용
        {
            Now_coin(P_coin, Bet);                                                      // 현재 금액, 배팅금액 안내

            Console.Write("딜러의 카드: \n" + card.d_patten[0] + " " + card.d_s_number[0] + ", ");
            Console.WriteLine("그리고 가려진 카드 한장 입니다. \n");                     // 딜러 카드 공
            Console.WriteLine("합: " + card.d_number[0] + " + " + "가려진 카드");
            Console.WriteLine("\n\n\n");

            Console.Write("플레이어의 카드: \n" + card.p_patten[0] + " " + card.p_s_number[0] + ", ");
            Console.WriteLine(card.p_patten[1] + " " + card.p_s_number[1] + "\n");      // 플레이어 카드 공개
        }

        public void D_open(ref Card card)
        {
            Console.WriteLine("\n딜러의 카드를 오픈하겠습니다.\n");         
            Thread.Sleep(1500);
            Console.WriteLine("딜러의 카드는 " + card.d_patten[0] + " " + card.d_s_number[0] + ", " + card.d_patten[1] + " " + card.d_s_number[1]);
            Console.WriteLine("합: " + d_total);
            Thread.Sleep(1000);
            int index = 0;
            ReADD:
            if (!p_bj)
            {
                while (d_total < 17)
                {
                    Console.WriteLine("\n\n딜러의 카드가 17보다 작으므로 카드를 추가하겠습니다. \n");
                    d_hit_count++;
                    Dealer_hit(ref card);
                    Total(ref d_total, card.d_number[1 + d_hit_count]);
                    D_Hitsay(ref card);
                    Thread.Sleep(1000);
                }

                if (d_total >= 17 && d_total <= 20) { Thread.Sleep(1000); Compare(d_total, p_total, win); }
                else if (d_total == 21) { Thread.Sleep(1000); Console.WriteLine("딜러 블랙잭!!\n"); Thread.Sleep(1500); Compare(d_total, p_total, win); }
                else if (d_total >= 22)
                {
                    for (int i = index; i <= d_hit_count + 2; i++)
                    {
                        if (card.SwapA(ref card.d_number[i], card.d_s_number[i]) == true)             //A를 포함하는지 검사
                        {
                                                                    // flag가 0일때 (A가 처음일때)
                            d_total -= 10;
                            index = i + 1;
                            Console.WriteLine("딜러 카드의 A(에이스)를 1로 변환 중..."); Thread.Sleep(1500); Console.WriteLine("합: " + d_total); Thread.Sleep(1500);
                            continue;
                                                                
                        }
                        else                                                                        // A를 포함하지 않으면
                        {
                            continue;
                        }
                    }
                    if (d_total < 17) { goto ReADD; }
                    else if (d_total >= 17 && d_total <= 20) { Thread.Sleep(1500); Compare(d_total, p_total, win); }
                    else if (d_total == 21) { Thread.Sleep(1500); Console.WriteLine("\n딜러 블랙잭!!\n"); Thread.Sleep(1000); Compare(d_total, p_total, win); }
                    else if (d_total >= 22) { win = false; Thread.Sleep(1500); Compare(d_total, p_total, win); }
                }
            }
            else
            {
                Console.WriteLine("\n\n플레이어가 블랙잭이므로 승리하셨습니다. 배팅금액의 2.5배가 보상으로 주어집니다. ");
                Thread.Sleep(1000);
            }
            
        }
        public void D_open_bust(ref Card card)
        {
            Thread.Sleep(500);
            Console.WriteLine("\n딜러의 카드를 오픈하겠습니다.\n");
            Thread.Sleep(1500);
            Console.WriteLine("딜러의 카드는 " + card.d_patten[0] + " " + card.d_s_number[0] + ", " + card.d_patten[1] + " " + card.d_s_number[1]);
            Console.WriteLine("합: " + d_total);
            Console.WriteLine();
        }
        public void Compare(int d, int p, bool win)
        {
            if (d > p && win)                                           //딜러가 21이고 내가 20일때 win플래그가 true
            {
                Now_coin(P_coin, Bet);
                Thread.Sleep(1000);
                Console.WriteLine("\n딜러 카드의 합: " + d_total + "\n플레이어 카드의 합: " + p_total + "\n\n패배하셨습니다.\n\n배팅한 금액이 사라집니다.\n");
                D_win();
                Console.WriteLine();
                ReGame();
            }
            else if (d == p)
            {
                Now_coin(P_coin, Bet);
                Thread.Sleep(1000);
                Console.WriteLine("\n딜러 카드의 합: " + d_total + "\n플레이어 카드의 합: " + p_total + "\n\n비기셨습니다.\n\n배팅한 금액을 다시 돌려 받습니다.\n");
                Draw();
                Console.WriteLine();
                ReGame();
            }
            else if(d > p && !win)                                      //딜러가 25이고 내가 20일때 win플래그가 false
            {
                //Now_coin(P_coin, Bet);
                //Thread.Sleep(1000);
                //Console.WriteLine("\n딜러 카드의 합: " + d_total + "\n플레이어 카드의 합: " + p_total + "\n패배하셨습니다.\n\n배팅한 금액이 사라집니다.");
                //D_win();
                //Now_coin(P_coin);
                //ReGame();
                Now_coin(P_coin, Bet);
                Thread.Sleep(1000);
                Console.WriteLine("\n딜러 카드의 합: " + d_total + "\n플레이어 카드의 합: " + p_total + "\n\n플레이어가 승리했습니다.\n\n배팅금액의 2배가 보상으로 주어집니다.\n");
                P_win();
                Console.WriteLine();
                ReGame();
            }
            else if (d < p)
            {
                Now_coin(P_coin, Bet);
                Thread.Sleep(1000);
                Console.WriteLine("\n딜러 카드의 합: " + d_total + "\n플레이어 카드의 합: " + p_total + "\n\n플레이어가 승리했습니다.\n\n배팅금액의 2배가 보상으로 주어집니다.\n");
                P_win();
                Console.WriteLine();
                ReGame();
            }
        }

        public void D_Hitsay(ref Card card)
        {
            Console.Write("카드 추가중");
            for (int i = 0; i < 2; i++)
            {
                string a = ".";
                Console.Write(a);
                Thread.Sleep(1000);                                             // ...이 추가되는 애니메이션
            }
            Console.WriteLine("\n\n추가된 카드: " + card.d_patten[1 + d_hit_count] + " " + card.d_s_number[1 + d_hit_count]);
            Console.WriteLine("합: " + d_total);
            Console.WriteLine();
            Thread.Sleep(1000);
        }
        public void P_HitSay(ref Card card)
        {
            Console.Write("카드 추가중");
            for (int i = 0; i < 2; i++)
            {
                string a = ".";
                Console.Write(a);
                Thread.Sleep(1000);                                             // ...이 추가되는 애니메이션
            }
            Console.WriteLine("\n\n추가된 카드: " + card.p_patten[1+p_hit_count] + " " + card.p_s_number[1+p_hit_count]);
            Console.WriteLine("합: " + p_total);
            Console.WriteLine();
            Thread.Sleep(1000);
        }

        public void ReGame()
        {
            int flag = 1;
            do
            {
                Console.WriteLine("\n\n다시하시려면 n을 입력, 종료하시려면 exit를 입력 해주세요.");
                string ans;
                ans = Console.ReadLine();
                switch (ans)
                {
                    case "n":
                    case "N":
                        Program.flag = 1;
                        flag = 0;
                        break;
                    case "exit":
                    case "EXIT":
                        Program.flag = 0;
                        flag = 0;
                        break;
                    default:
                        flag = 1;
                        break;
                }
            }while (flag != 0);
        }

        public void D_BJ_say(ref Card card)
        {
            if (Bj_Check(p_total) == false)                                 // 플레이어 not 블랙잭
            {
                Console.WriteLine("딜러의 카드: \n" + card.d_patten[0] + " " + card.d_s_number[0 + d_hit_count] + ", " + card.d_patten[1] + " " + card.d_s_number[1] + "\n");
                Console.WriteLine("합: " + d_total);
                Console.WriteLine("\n\n\n");
                Console.WriteLine("플레이어의 카드: \n" + card.p_patten[0] + " " + card.p_s_number[0 + p_hit_count] + ", " + card.p_patten[1] + " " + card.p_s_number[1] + "\n");
                Console.WriteLine("합: " + p_total + "\n\n");
                Console.WriteLine("패배 하셨습니다. 배팅 한 코인은 사라집니다.");
                D_bj_win();
                ReGame();
            }
        }
        

        public void Play()
        {
            Card card = new Card();

            int p_random, d_random;
            for (int i = 0; i < 2; i++)
            {
                p_random = card.RandomNum();                                        // 카드위치 값
                d_random = card.RandomNum();
                card.p_number[i + p_hit_count] = card.GetCard(p_random);            // 카드 실제 숫자
                card.SwapItoP(ref card.p_number[i], ref card.p_s_number[i]);        // AKQJ 변환하기
                card.SuitDivide(p_random, ref card.p_patten[i]);                    // 문양 받아오기

                card.d_number[i + d_hit_count] = card.GetCard(d_random);
                card.SwapItoP(ref card.d_number[i], ref card.d_s_number[i]);
                card.SuitDivide(d_random, ref card.d_patten[i]);
            }

            p_total = card.p_number[0] + card.p_number[1];                      // 첫판 각 숫자의 합
            d_total = card.d_number[0] + card.d_number[1];

            Now_coin(P_coin, Bet);                                              // 현재 금액, 배팅금액 안내
            Console.WriteLine("카드를 분배합니다.\n\n");
            Thread.Sleep(1000);
            Console.Write("딜러의 카드: \n" + card.d_patten[0] + " " + card.d_s_number[0 + d_hit_count] + ", ");
            Thread.Sleep(1500);
            Console.WriteLine("그리고 가려진 카드 한장 입니다. \n");              // 딜러 카드 공개
            Thread.Sleep(500);
            Console.WriteLine("합: " + card.d_number[0] + " + " + "가려진 카드");
            Console.WriteLine("\n\n\n");
            Thread.Sleep(1500);

            Console.Write("플레이어의 카드: \n" + card.p_patten[0] + " " + card.p_s_number[0 + p_hit_count] + ", ");
            Thread.Sleep(1500);
            Console.WriteLine(card.p_patten[1] + " " + card.p_s_number[1] + "\n"); // 플레이어 카드 공개
            Thread.Sleep(500);
            Console.WriteLine("합: " + p_total +"\n\n");
            Thread.Sleep(1500);

            if (Bj_Check(p_total))                                          // 플레이어 블랙잭 여부
            {
                p_bj = true;
                P_blacksay();
                P_bj_win();
                ReGame();
                return;
            }

            if (card.d_s_number[0] == "A" || card.d_s_number[0] == "K" || card.d_s_number[0] == "Q" || card.d_s_number[0] == "J" || card.d_s_number[0] == "10")
            {
                Console.Write("딜러 블랙잭 여부 확인중");
                for (int i = 0; i < 3; i++)
                {
                    string a = ".";
                    Console.Write(a);
                    Thread.Sleep(1000);                                             // ...이 추가되는 애니메이션
                }
            }
            if(Bj_Check(d_total))
            {
                Console.WriteLine();
                Console.WriteLine("\n딜러 블랙잭!!\n");
                D_BJ_say(ref card);
                return;
            }
            Console.WriteLine();
            Console.WriteLine();
            string ans;
            int index = 0;
            Re:
            Console.WriteLine("\n카드를 추가 하시겠습니까? 추가하시려면 H를, 멈추시려면 S를 입력 해 주세요.");
            ans = Console.ReadLine();
            switch (ans)
            {
                case "h":
                case "H":
                    p_hit_count++;                                                                      // 히트한 횟수 올리고
                    Console.WriteLine();
                    Player_hit(ref card);                                                               // 값 받고(카드추가)
                    Total(ref p_total, card.p_number[1+p_hit_count]);                                   // 플레이어 카드 합 계산

                    if (p_total < 21) { P_HitSay(ref card); goto Re; }                                    // 21보다 작을때 카드출력후 히트여부
                    else if (p_total == 21) { P_HitSay(ref card); P_blacksay_hit(); Dealer_hit(ref card); Player_stay(ref card); }   // 블랙잭 일때 
                    else {                                                                              // 21이상일 때
                        for(int i= index; i <= p_hit_count+2; i++)                                      //추가한 카드 + 원래카드
                        {
                            if (card.SwapA(ref card.p_number[i], card.p_s_number[i]) == true)             //A를 포함하는지 검사
                            {                                   
                                index = i+1;
                                p_total -= 10;
                                continue;
                            }
                            else                                                                        // A를 포함하지 않으면
                            {
                                continue; 
                            }                               
                        }

                        if (p_total < 21) { P_HitSay(ref card); goto Re; }                  // A를 변환했을때 21보다 이하면
                        else if (p_total == 21) { P_HitSay(ref card); P_blacksay_hit(); Dealer_hit(ref card);} // A를변환했을때 블랙잭이면
                        else { P_HitSay(ref card); D_open_bust(ref card); P_bust(); D_win(); ReGame(); }                     // 21이상이면 버스트 출력후 배팅금액 삭제
                    }
                    break;

                case "s":
                case "S":
                    Player_stay(ref card);
                    break;
                default:                                                        // 잘못 입력했을 때
                    goto Re;
            }

            // 비교
        
         


        }

    }
}
