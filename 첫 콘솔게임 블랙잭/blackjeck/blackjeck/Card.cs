using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjeck
{
    public class Card
    {
        public string[] m_suit = { "♤(스페이드)", "♡(하트)", "◇(다이아)", "♧(클로버)" };
        public int[] m_number;                                           // 메인카드
        public int[] p_number = new int[11];                                           // 플레이어가 받은카드
        public int[] d_number = new int[11];                                           // 딜러가 받은 카드
        public string[] p_patten = new string[128];                                     // 플레이어의 문양         84
        public string[] d_patten = new string[128];                                     // 딜러의 문양
        public string[] p_s_number = new string[128];                                   // 실제 출력될 플레이어 카드번호
        public string[] d_s_number = new string[128];                                   // 실제 출력될 딜러 카드번호

        public Card()
        {
            m_number = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13             // Spades          black
                                , 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13             // Heart            red
                                , 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13             // Diamond          red
                                , 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};           // Club          black  A = 1  J = 11 Q = 12 K = 13
        }

        public int RandomNum()                                                          // 카드 번호 분배기
        {
            int num;
            Random rand = new Random();
            num = rand.Next(0, 51);
            while (m_number[num] == 0)                                                  // 같은 수가 나올 경우의 수 차단
            {
                num = rand.Next(0, 51);
            }
            m_number[num] = 0;                                                          // 중복 카드를 뽑지 못하게 0으로 설정
            return num;
        }

        public void SuitDivide(int num, ref string suit)                                // 문양 정하기
        {
            if (num < 13) suit = m_suit[0];
            else if (num >= 13 && num < 26) suit = m_suit[1];
            else if (num >= 26 && num < 39) suit = m_suit[2];
            else suit = m_suit[3];
        }
        public int GetCard(int num)
        {
            Card a = new Card();
            int card = a.m_number[num];
            return card;
        }

        public bool SwapA(ref int num, string A)                                    // A가 포함되면 true반환후 값 1로 변환
        {
            if (A == "A")
            {
                num = 1;
                return true;
            }
            return false;
            
        }

        public void SwapItoP(ref int card, ref string s_card)                           // 숫자에서 문자로 변환
        {
            switch (card) {
                case 1:
                    s_card = "A";
                    card = 11;
                    break;

                case 11:
                    s_card = "J";
                    card = 10;
                    break;

                case 12:
                    s_card = "Q";
                    card = 10;
                    break;
                case 13:
                    s_card = "K";
                    card = 10;
                    break;
                default:
                    s_card = Convert.ToString(card);
                    break;
            }

        }

    }
}
