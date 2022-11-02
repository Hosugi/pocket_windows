using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace blackjeck
{

    public class Coin
    {
        public static double P_coin;
        //{
        //    get { return P_coin; }
        //    set { P_coin = value; }
        //}
        public double Bet;
        //{
        //    set { Bet = value; }
        //    get { return Bet; }
        //}

        public Coin()
        {
                           // init 선언은 곧 초기화
        }
        public void Betting(int bet)
        {

            P_coin = P_coin - bet;
            Bet = bet;
        }
        public void P_bj_win()
        {
            P_coin = P_coin + (Bet * 2.5);
        }
        public void D_bj_win()
        {
            Bet = 0;
        }
        public void Draw()
        {
            P_coin = P_coin + Bet;
        }
        public void P_win()
        {
            P_coin = P_coin + (Bet * 2);
        }
        public void D_win()
        {
            Bet = 0;
        }
        public bool MoreCoin(double p_coin, double bet)                     // 가지고 있는 코인보다 배팅이 많을 경우 false 반환
        {
            if (p_coin < bet)
            {
                return false;
            }
            return true;
        }
        public void Now_coin(double coin)                                   // 배팅 전 보유코인 안내
        {
            Console.WriteLine("현재 보유 코인은 " + coin + " 코인 입니다.");
        }
        public void Now_coin(double coin, double bet)                       // 배팅 후 보유코인, 배팅금액
        {
            Console.Clear();
            Console.WriteLine("보유한 코인: " + coin + "\n\n배팅한 금액: " + bet);
            Console.WriteLine("\n\n\n\n");
        }

        public void Check_bust()
        {
            if (P_coin > 0) Program.Gameover();
        }

        public bool GameOver_coin(double coin)
        {
            if (coin == 0)
            { return true; }
            return false;            
        }
    }
}
