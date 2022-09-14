using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace baekjoonQ
{
    public class Avg
    {
        public string s_loop, s_inputloop;
        public int loop, input_loop, i;                 // i는 반복문 인덱싱
        public int total, count;
        public string[] s_arr;
        public string ex;
        public int[] arr = new int[1002];               // 실제 데이터가 담길 배열
        public float avg, total_avg;
        public Avg()
        {
            s_loop = Console.ReadLine();            //1 문자열로 먼저받고
            loop = Convert.ToInt32(s_loop);         //2 정수로 변환
            total_avg = 0;                          //3 RAII 
            Loop();                                 //4 학생수 값 받기
        }
        public void Loop()
        {
            for (int i = 0; i < loop; i++)          //14 다시 loop값대로 for문 복귀
            {
                Input();                            //5 입력받으러가기 
                Calc();                             //8 total 받았으면 계산 하러가고  -> 45줄
                Print();                            //12 출력하기 
            }
        }
        public void Input()
        {
            total = 0;
            ex = Console.ReadLine();
            s_arr = ex.Split(' ');
            arr[0] = Convert.ToInt32(s_arr[0]);
            for(i = 1; i <= arr[0]; i++)             //6 학생 수 받고
            {
                arr[i] = Convert.ToInt32(s_arr[i]);                                  // 6-1 공백 포함 받고
            }
            for (int j = 1; j < i; j++)
            {
                total += arr[j];                        //7 그만큼 점수 입력받아서 total에 넣기
            }
        }
        public void Calc()
        {
            count = 0;
            avg = total / arr[0];                           //9 비교할 평균점수 구하기 
            for (int k = 1; k < i; k++)
            {
                if (avg < arr[k]) count++;                           //10 받은 점수중에 평균보다 높은 사람 카운트 하고
            }        
            total_avg = ((float)count / arr[0]) * 100;                     //11 float 으로 강제(캐스팅) 학생수만큼 평균
        }
        public void Print()
        {
            Console.Write("{0:F3}", total_avg);          //13 출력
            Console.WriteLine("%");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Avg avg = new Avg();
        }
    }
}
