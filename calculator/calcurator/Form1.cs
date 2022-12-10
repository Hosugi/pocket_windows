using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace calculator
{
    public partial class Form1 : Form
    {
        StringBuilder sb = new StringBuilder();                          // 식을 입력 받는 스트링
        StringBuilder num = new StringBuilder();                         // 후위 연산에서 피연산자를 담는 스트링
        public char[] operatorArr = new char[128];                       // 오퍼레이터를 담을 스택
        public double[] ans = new double[128];
        public string[] ABCD = new string[128];                          // 오퍼레이터를 만났을 때 숫자를 잘라서 넣을 string 배열 
        int arrtop = 0;                                                  // 스택의 최상위 인덱스 위치 (실제 값은 -1만큼 아래에있음)
        int len = 0;                                                     // 입력받는 길이 체크
        bool opflag = false;                                             // true = 오퍼레이터 사용 가능  false = 오퍼레이터 사용 불가능 (중복 오퍼레이터 사용을 방지)
        bool comma_flag = true;                                          // true = 콤마 사용가능  false = 이미 식 안에 콤마 사용
        bool numflag = false;                                            // false = 오퍼레이터 한번도 사용안하고 자연수 그자체 true = 오퍼레이터 사용함.
        int bra_cnt = 0;                                                 // 괄호 사용 갯수 체크
        bool power = false;                                              // 계산기 켜고 끄는 버튼 제어
        bool firstExe = false;                                           // 처음실행 시 버튼을 누를 때 종료 알림 제어 
        bool zeroflag = false;

        //enum Oper
        //{
        //    LEFT_BRACKET = '(',             //40
        //    RIGHT_BRACKET = ')',            //41
        //    MULTIPLY = '*',                 //42
        //    DIVIDE = '/',
        //    MOD = '%',
        //    ADD = '+',
        //    SUB = '-',
        //    SPACE = ' ',
        //    OPERAND                         //피연산자
        //}


        public Form1()
        {
            InitializeComponent();

        }
        private int Check(char oper)
        {
            if (oper == '(' || oper == ')')
            {
                return 3;
            }
            else if (oper == '*' || oper == '/')
            {
                return 1;
            }
            else if (oper == '%')
            {
                return 1;
            }
            else if (oper == '+' || oper == '-')
            {
                return 2;
            }
            return -1;
        }
        private void KeyCheck(ref object sender, ref KeyEventArgs e)
        {   // 키보드를 눌렀을 때 값을 직접 입력하는 것이 아닌 버튼을 누르는 이벤트를 발생시킴.
            //listBox1.Items.Add(e.KeyCode);
            if (!power)
            {
                if (e.Shift && e.KeyCode == Keys.Oemplus)
                {
                    btAdd_Click(sender, e);
                }
                else if (e.Shift && e.KeyCode == Keys.D8)
                {
                    btMul_Click(sender, e);
                }
                else if (e.Shift && e.KeyCode == Keys.D5)
                {
                    btMod_Click(sender, e);
                }
                else if (e.Shift && e.KeyCode == Keys.D9)
                {
                    btBracket1_Click(sender, e);
                }
                else if (e.Shift && e.KeyCode == Keys.D0)
                {
                    btBracket2_Click(sender, e);
                }
                else
                {
                    switch (e.KeyCode)
                    {
                        case Keys.D1:
                        case Keys.NumPad1:
                            bt1_Click(sender, e);
                            break;
                        case Keys.NumPad2:
                        case Keys.D2:
                            bt2_Click(sender, e);
                            break;
                        case Keys.NumPad3:
                        case Keys.D3:
                            bt3_Click(sender, e);
                            break;
                        case Keys.NumPad4:
                        case Keys.D4:
                            bt4_Click(sender, e);
                            break;
                        case Keys.NumPad5:
                        case Keys.D5:
                            bt5_Click(sender, e);
                            break;
                        case Keys.NumPad6:
                        case Keys.D6:
                            bt6_Click(sender, e);
                            break;
                        case Keys.NumPad7:
                        case Keys.D7:
                            bt7_Click(sender, e);
                            break;
                        case Keys.NumPad8:
                        case Keys.D8:
                            bt8_Click(sender, e);
                            break;
                        case Keys.NumPad9:
                        case Keys.D9:
                            bt9_Click(sender, e);
                            break;
                        case Keys.NumPad0:
                        case Keys.D0:
                            bt0_Click(sender, e);
                            break;
                        case Keys.Back:
                            del_Click(sender, e);
                            break;
                        case Keys.OemQuestion:
                            btDiv_Click(sender, e);
                            break;
                        case Keys.OemPeriod:                     // dot key
                            btComma_Click(sender, e);
                            break;
                        case Keys.Enter:
                        case Keys.Oemplus:
                            btEqual_Click(sender, e);
                            break;
                        case Keys.OemMinus:
                            btSub_Click(sender, e);
                            break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                return;
            }
        }
        private void AllReset()
        {
            sb.Clear();
            num.Clear();
            ans.Initialize();
            textBox1.Text = "0";
            len = 0;

            opflag = false;
            comma_flag = true;
            numflag = false;
            bra_cnt = 0;
            arrtop = 0;
            zeroflag= false;

            operatorArr.Initialize();
        }

        private void AllReset(bool status)
        {
            sb.Clear();
            string a = ans[0].ToString();
            for (int i = 0; i < a.Length; i++)
            {
                sb.Append(a[i]);
            }

            num.Clear();
            ans.Initialize();
            len = 0;
            opflag = true;
            comma_flag = true;
            numflag = false;
            zeroflag = false;
            bra_cnt = 0;
            arrtop = 0;
            operatorArr.Initialize();
        }
        private bool ConvertInt()                                   // false = operator  true = Number
        {
            int a = Convert.ToInt32(sb[sb.Length - 1]);             // 스트링 끝의 단어를 숫자로 변환했을 때 오퍼레이터면 숫자가 48이하로 뜸
            if (a < '0') { return false; }                           // false = 오퍼레이터
            return true;                                            // true = 숫자
        }

        private bool ConvertInt(int index)                          // false = oper  true = Number  index = 찾을 인덱스 위치
        {
            int a = Convert.ToInt32(sb[sb.Length - (index + 1)]);   //스트링 끝의 단어를 숫자로 변환했을 때 오퍼레이터면 숫자가 48이하로
            if (a < '0') { return false; }                          // false = 오퍼레이터
            return true;                                            // true = 숫자
        }

        private bool ConvertInt(char c)
        {
            int a = Convert.ToInt32(c);
            if (a < '0') { return false; }
            return true;
        }

        //private int Getrank(char op, int stackindex)
        //{
        //    //oprank는 우선순위이며 0순위부터 3순위 까지 존재함.
        //    int oprank = -1;
        //    switch (op)
        //    {
        //        case (char)Oper.LEFT_BRACKET:
        //            if (stackindex == 0) { oprank = 0; }                // 첫번째 괄호일 때 //제일 높은 우선순위
        //            else { oprank = 3; }                                // 두번째 괄호일 때 //괄호안의 수식 다 계산 후에 동작해야 하므로 낮은 순위
        //            break;
        //        case (char)Oper.MULTIPLY:
        //        case (char)Oper.DIVIDE:
        //            oprank = 1;
        //            break;
        //        case (char)Oper.ADD:
        //        case (char)Oper.SUB:
        //            oprank = 2;
        //            break;
        //    }
        //    return oprank;
        //}


        public void pop()
        {
            while (arrtop != 0)
            {
                num.Append(operatorArr[--arrtop]);      //스택의 최상위 인덱스를 -1한후 그 위치의 값을 추가한다
                operatorArr[arrtop] = '\0';             //그 위치 값을 지운다
            }

        }
        public void pushTop(char op)
        {
            operatorArr[arrtop++] = op;

        }

        private void pop_bra()
        {                        //닫는괄호가 나오기전 오퍼레이터가 2개 쌓였을 때
            if (arrtop <= 0) { return; }
            else
            {
                for (; operatorArr[arrtop - 1] != '(';)
                {
                    num.Append(operatorArr[arrtop - 1]);
                    operatorArr[--arrtop] = '\0';
                    // 괄호안에 오퍼레이터를 pop 하여 추가한다.   
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bt_power_Click(sender, e);

        }

        private bool rightBraCheck()
        {
            if (sb.Length > 0 && sb[sb.Length - 1] == ')')
            {
                return true;
            }
            return false;
        }
        private void bt7_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')                   // 처음 스트링에 0을 넣고 숫자를 누를 때 0을 무시하고 누른 값 출력 
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('7');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
            else if (sb.Length > 1 && sb[sb.Length - 1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('7');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('7');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }


        }

        private void bt8_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('8');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
            else if (sb.Length > 1 && sb[sb.Length - 1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('8');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('8');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
        }

        private void bt9_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('9');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
            else if (sb.Length > 1 && sb[sb.Length - 1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('9');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('9');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('4');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
            else if (sb.Length > 1 && sb[sb.Length - 1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('4');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('4');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('5');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
            else if (sb.Length > 1 && sb[sb.Length - 1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('5');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('5');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('6');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
            else if (sb.Length > 1 && sb[sb.Length - 1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('6');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('6');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('1');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else if (sb.Length > 1 && sb[sb.Length - 1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('1');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('1');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('2');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
            else if (sb.Length > 1 && sb[sb.Length - 1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('2');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('2');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (sb.Length == 1 && sb[0] == '0')
            {
                opflag = true;
                len--;
                sb.Clear();
                sb.Append('3');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
            else if (sb.Length > 1 && sb[sb.Length-1] == '0' && !ConvertInt(sb[sb.Length - 2]))
            {
                sb.Remove(sb.Length-1,1);
                sb.Append('3');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();

            }
            else
            {
                opflag = true;
                sb.Append('3');
                zeroflag = true;
                len++;
                textBox1.Text = sb.ToString();
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            AllReset();
            BtnOn();
        }

        private void bt0_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            if (textBox1.Text == "0" && sb.Length == 0)                  //아무것도 안눌렀을 때 0 클릭
            {
                opflag = true;
                sb.Append('0');
                zeroflag = false;
                textBox1.Text = sb.ToString();
                len++;
            }
            else if ((textBox1.Text == "0" && sb[0] == '0') || !zeroflag)              //0. 소수점을 입력하려 할 때
            {
                opflag = true;
                textBox1.Text = sb.ToString();
            }
            else                                                        // 일반적인 0입력
            {
                if ((comma_flag && sb[sb.Length-1] == '0') && (!ConvertInt(sb[sb.Length-2])))
                {
                    //if (sb[sb.Length - 2] != '(' && sb[sb.Length - 2] != ')'))
                    MessageBox.Show("0을 연속해서 사용 할 수 없습니다.","알림");
                    sb.Remove(sb.Length - 1, 1);
                    textBox1.Text = sb.ToString();
                    return;
                }
                else
                {
                    opflag = true;
                    sb.Append('0');
                    len++;
                    textBox1.Text = sb.ToString();
                }
            }
        }

        private void btComma_Click(object sender, EventArgs e)
        {
            if (sb.Length == 0 && !opflag && !comma_flag)
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            else if (sb.Length == 0 && comma_flag)
            {
                sb.Append('0');
            }

            if (comma_flag)
            {
                if (sb[sb.Length - 1] < 48)               // 콤마를 사용 가능하지만 콤마앞에 숫자가 없거나 -기호일 때)
                {
                    //MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                    //return;
                    sb.Append('0');
                }
                sb.Append('.');
                len++;
                zeroflag= true;
                textBox1.Text = sb.ToString();
                opflag = false;
                comma_flag = false;
            }
            else
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }

        }

        private void btBracket1_Click(object sender, EventArgs e)
        {
            if ((sb.Length > 0) && sb[sb.Length - 1] == '.')
            {
                sb.Append('0');
            }
            bra_cnt++;                                                          //괄호생성할때마다 증가,  0이 되면 괄호 끝
            sb.Append('(');
            len++;
            if ((sb.Length > 1 && ConvertInt(1)) || (sb.Length > 1 && sb[sb.Length - 2] == ')'))   // 길이가 0이 아니면서 괄호의 앞이 숫자일 때, 괄호의 앞이 닫는괄호일 때                  
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('*');                                                 // 자동으로 곱하기로 인식
                numflag= true;                                                  // 오퍼레이터가 사용됬다고 인식
                sb.Append('(');
                len++;
            }

            // 괄호 시작
            opflag = false;
            comma_flag = true;
            textBox1.Text = sb.ToString();

            // oper arr 에 집어넣는 코드를 추가 해야 할 것 같음.
            // 숫자 따로 후위 식으로 정렬 후 
            // 그러고 나서 rank oper 체크 후 연산자 우선순위에 맞게 또 정렬. 
        }

        private void btBracket2_Click(object sender, EventArgs e)
        {
            if (bra_cnt == 0)                                                      // 괄호 시작을 안했을 때
            {
                MessageBox.Show("여는 괄호를 먼저 사용 해주세요.", "알림");
                return;
            }
            if (!ConvertInt(sb[sb.Length - 1]) && sb[sb.Length - 1] != '/' && sb[sb.Length - 1] != ')')       // 버그 발생 확률 ㅈㄴ높음
            {
                sb.Append('0');
            }
            else if (sb[sb.Length - 1] < 48 && sb[sb.Length - 1] == '/')          // 나머지는 0을 지움
            {
                sb.Remove(sb.Length - 1, 1);
            }

            sb.Append(')');
            len++;
            bra_cnt--;                                                              // 닫는괄호 사용했으니 여는괄호 1개 제거.

            if (bra_cnt >= 0 && sb[sb.Length - 2] == '(')                         // 여는괄호 다음 바로 닫는괄호 쓸 때
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append('0');
                sb.Append(')');
                len++;
            }
            if (bra_cnt == 0 && !opflag && !ConvertInt() && sb[sb.Length - 1] != ')') // 괄호를 닫을 때 그 전에 오퍼레이터면서 닫는 괄호가 아닐 때
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                sb.Remove(sb.Length - 1, 1);
                len--;
                return;
            }


            textBox1.Text = sb.ToString();
            // 괄호 닫기
            opflag = true;                                                      // 괄호 닫은후 숫자가 들어가면 어색 함
        }

        private void btMul_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || !opflag)
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            sb.Append('*');
            opflag = false;
            comma_flag = true;
            numflag = true;
            len++;
            textBox1.Text = sb.ToString();
        }

        private void btSub_Click(object sender, EventArgs e)
        {
            try
            {
                if (bra_cnt > 0)
                {
                    opflag = true;
                    comma_flag = true;
                    //goto BRA;
                }

                if (sb.Length != 0 && !opflag)
                {
                    MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                    return;
                }
                else if( sb.Length != 0 && sb[sb.Length-1] == '(')
                {
                    MessageBox.Show("음수 연산 미구현", "알림");
                    return;
                }
                else if (sb.Length == 0 && !opflag)
                {
                    MessageBox.Show("음수 연산 미구현", "알림");
                    return;
                }

                if (sb.Length != 0)
                {
                    int a = Convert.ToInt32(sb[sb.Length - 1]);             //스트링 끝의 단어를 숫자로 변환했을 때 오퍼레이터면 숫자가 제데로 안 뜸
                    if (a < '0' && sb[sb.Length - 1] != ')')                   // 숫자가 아니면서 닫는 괄호가 아닐 때
                    {
                        throw new Exception();
                    }
                    opflag = true;
                    numflag = true;
                    comma_flag = true;
                }
                else
                {
                    opflag = true;                                  //음수로 시작하는 경우의 수
                    numflag = false;                                //음수 그대로 출력 해야하므로 (오퍼레이터가아님)
                    comma_flag = true;
                }
            }
            catch
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                opflag = false;
                return;
            }

            opflag = false;

        //BRA:
            sb.Append('-');
            numflag = true;
            len++;
            textBox1.Text = sb.ToString();
        }

        private void btDiv_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || !opflag)
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            sb.Append('/');
            opflag = false;
            numflag = true;
            comma_flag = true;
            len++;
            textBox1.Text = sb.ToString();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || !opflag)
            {
                MessageBox.Show("양수를 나타내려면 숫자만 기입하세요.", "알림");
                return;
            }
            sb.Append('+');
            opflag = false;
            numflag = true;
            comma_flag = true;
            len++;
            textBox1.Text = sb.ToString();
            //now_formula.Text = sb
        }

        private void btMod_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || !opflag)
            {
                MessageBox.Show("표현식이 잘못되었습니다.", "알림");
                return;
            }
            sb.Append('%');
            numflag = true;
            opflag = false;
            comma_flag = true;
            len++;
            textBox1.Text = sb.ToString();
        }

        private void btEqual_Click(object sender, EventArgs e)
        {
            try
            {
                num.Clear();
                // 예외처리 과정
                if (sb.Length == 0)
                {
                    return;
                }
                else if (sb[sb.Length - 1] == '.' || (!ConvertInt(sb[sb.Length - 1]) && sb[sb.Length - 1] != '/' && sb[sb.Length -1] != ')')) // 수식의 끝이 오퍼레이터나 .로 끝나면 0을 채우기.
                {
                    //sb.Append(' ');
                    sb.Append('0');
                    
                }
                else if (!ConvertInt(sb[sb.Length - 1]) && sb[sb.Length - 1] == '/')                            // 0으로 나눌 수 없으니 나누기 오퍼레이터 제거   
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                for (; bra_cnt > 0; bra_cnt--)                                   // 괄호 안닫고 = 눌렀을 때
                {
                    sb.Append(')');
                }


                if (!numflag)                                                    // 자연수 자체만 들어왔을 때
                {
                    if (sb[0] == '(') { sb.Remove(0, 1); sb.Remove(sb.Length - 1, 1); }            //괄호가 같이 들어왔을 때
                    listBox1.Items.Add(sb.ToString());
                    return;
                }

                // 후위연산식으로 바꾸는 과정
                int braCheck = 0;
                bool braflag = false;
                for (int i = 0; i < sb.Length; i++)
                {
                    if (braCheck > 0)                                                //여는 괄호가 더 있다면 플래그를 true로.
                    {
                        braflag = true;
                    }
                    if ((sb[i] >= '0' && sb[i] <= '9') || sb[i] == '.')
                    {
                        num.Append(sb[i]);                                          // 숫자, 소숫점 모두 무조건 추가
                    }
                    else
                    {
                        switch (sb[i])                                              // 오퍼레이터 체크
                        {
                            case ' ':
                                break;
                            case '(':
                                pushTop(sb[i]);
                                braCheck++;                                         //여는괄호 갯수 증가
                                break;
                            case '*':
                            case '/':
                            case '%':
                            case '+':
                            case '-':
                                num.Append(' ');    // 중위연산식에서 오퍼레이터를 만나면 후위연산식의 끝에 공백을 추가하여 각 숫자를 구분한다. 
                                if (arrtop == 0) { pushTop(sb[i]); break; }
                                else if (!braflag && arrtop != 0 && Check(sb[i]) >= Check(operatorArr[arrtop - 1]))
                                {// 1부터 3까지 우선순위, 1이 가장 높음                ( , ) = 3    *, /, % = 1    +, - = 2   플래그가 false, 즉 괄호안에서 추가하는게 아닐때
                                    pop();
                                    pushTop(sb[i]);
                                }
                                else if (!braflag && arrtop != 0 && Check(sb[i]) < Check(operatorArr[arrtop - 1]))   //스택안의 연산자 우선순위가 더 낮을 때
                                {
                                    pushTop(sb[i]);
                                }
                                else if (braflag && arrtop != 0 && Check(sb[i]) >= Check(operatorArr[arrtop - 1])) //플래그가 true, 괄호안에서 push, 닫는 괄호가 나올 때 까지....
                                {
                                    pop_bra();
                                    pushTop(sb[i]);
                                }
                                else if (braflag && arrtop != 0 && Check(sb[i]) < Check(operatorArr[arrtop - 1]))
                                {
                                    pushTop(sb[i]);
                                }
                                break;
                            case ')':
                                //if(i == sb.Length) { return; }
                                pop_bra();
                                if (operatorArr[arrtop - 1] == '(')                                                    // 처음 시작이 괄호이면
                                {
                                    operatorArr[--arrtop] = '\0';

                                }
                                // 괄호안에 오퍼레이터를 pop 하여 추가한다.   

                                //pop();
                                braCheck--;                                                     //여는괄호 개수 줄이기
                                braflag = false;                                                //일단 괄호 빠져나왔으니 false, 하지만 여는괄호의 갯수가 많을 수도있다.
                                break;
                        }
                    }
                }
                //식 끝났을 때 oper arr 에 남아있는 값 append 하기
                for (int i = arrtop - 1; i >= 0; i--) { pop(); }


                listBox2.Items.Add(num.ToString());                                             // 후위연산식 저장



                string abcd = "";
                int loop = 0;
                int top = 0;
                bool comflag = false;                                               // 검사할 숫자가 부동소수인지 체크하는 플래그
                while (loop <= num.Length - 1)
                {
                    while (num[loop] != ' ' && (num[loop] == '.' || ConvertInt(num[loop])))  //문자열이 공백이거나 오퍼레이터면 멈춤
                    {
                        if (num[loop] == '.')
                        {
                            comflag = true;
                        }

                        abcd += num[loop];
                        loop++;
                    }

                    if (num[loop] == ' ' || (!ConvertInt(num[loop]) && num[loop] != '.'))        // 다음이 공백이면서 오퍼레이터일때
                    {
                        if (abcd != "" && comflag)                                                    // 숫자가 이미 부동소수이면
                        {
                            ans[top++] = Convert.ToDouble(abcd);                          // 바로 변환
                        }
                        else if (abcd != "" && !comflag)
                        {                                                               // 숫자가 정수면
                            ulong a = Convert.ToUInt64(abcd);                              // unsigned long으로 변환한 뒤 (그냥 변환하면 X.1322314 이렇게 이상한 값 붙음)
                            ans[top++] = Convert.ToDouble(a);                             // double로 변환
                        }
                    }

                    if (!ConvertInt(num[loop]) && num[loop] != '.' && num[loop] != ' ')             // 식이 오퍼레이터일 때
                    {                                                                      // 앞에서 값 추가했으니 일단 top++
                        ans[top - 2] = math(ans[top - 2], ans[top - 1], num[loop]);
                        ans[top - 1] = '\0';
                        top--;
                        loop++;
                        abcd = "";
                        continue;
                    }
                    else
                    {
                        loop++;
                        abcd = "";
                    }
                }
                listBox1.Items.Add(sb + "=" + ans[0].ToString());                               // 중위연산식 저장
                textBox1.Text = ans[0].ToString();
                if(((textBox1.Text.Length > 1 ) && (textBox1.Text == "NaN" || ans[0] < 0 || textBox1.Text[1] == 'E')) || textBox1.Text == "∞")
                {   // 결과가 무한이거나  길이가 1 이상이면서 nan , 음수, 범위를 넘어가는 E 연산이 나올때
                    BtnOff_Negative();
                }
                else if ((textBox1.Text.Length > 1 && textBox1.Text[1] == '.'))
                {
                    for(int i =0; i < textBox1.Text.Length; i++)
                    {
                        if(textBox1.Text[i] == 'E')
                        {
                            BtnOff_Negative();
                        }
                    }
                }
                //ans[top] = '\0';
                top--;
                //콤마, 괄호 등등 플래그 원래대로 초기화, sb를 제외한 나머지 스트링 초기화
                AllReset(true);
                //AllReset();
            }
            catch
            {
                MessageBox.Show("식이 제데로 완성되지 않았습니다.");
                return;
            }
        }

        private void del_Click(object sender, EventArgs e)
        {
            if (rightBraCheck())
            {
                bra_cnt--;
            }
            // 오퍼레이터 지웠는데 길이가 0일 때 
            if (sb.Length > 1 && sb[sb.Length - 1] == '.') { comma_flag = true; }
            for (int i = 0; i < sb.Length; i++)
            {
                if (!sb[i].Equals('+') || !sb[i].Equals('-') || !sb[i].Equals('*') || !sb[i].Equals('/') || !sb[i].Equals('%'))
                {
                    numflag = false;
                }
            }
            if (len == 1 && textBox1.Text == "0") { sb.Clear(); textBox1.Text = "0"; len = 0; return; }              //0버튼을 누른뒤 del을 눌렀을 때
            else if (sb.Length == 0) { textBox1.Text = "0"; opflag = false; return; }
            if (sb[sb.Length-1] == '(')
            {
                zeroflag = false;
            }
            sb.Remove(sb.Length - 1, 1);
            textBox1.Text = sb.ToString();
            len--;

            int cntA = 0, cntB = 0;                                // 괄호 쌍 카운트

            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i].Equals('('))
                {
                    cntA++;
                }
                if (sb[i].Equals(')'))
                {
                    cntB++;
                }
            }

            if (sb.Length > 0 && cntA > cntB)
            {
                bra_cnt = cntA - cntB;
                cntA = 0;
                cntB = 0;
            }
            else if (sb.Length > 0 && cntA == cntB)
            {
                bra_cnt = 0;
                cntA = 0;
                cntB = 0;
            }
            else if (sb.Length == 0 && cntA > cntB)
            {
                bra_cnt = 0;
                cntA = 0;
                cntB = 0;
            }



            if (sb.Length != 0)
            {
                int a = Convert.ToInt32(sb[sb.Length - 1]);             //스트링 끝의 단어를 숫자로 변환했을 때 오퍼레이터면 숫자가 제데로 안뜸
                if (a < '0' && sb[sb.Length - 1] != ')')                                 // 숫자가 아닐 때
                {
                    opflag = false;
                    return;
                }
                else
                {
                    opflag = true;
                    return;
                }
            }


            //if (pointMode) { pointMode = false; }
            if (sb.Length == 0) { textBox1.Text = "0"; opflag = false; len = 0; bra_cnt = 0; return; }
            textBox1.Text = sb.ToString();
        }

        private double math(double num1, double num2, char op)
        {
            switch (op)
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case '*':
                    return num1 * num2;
                case '/':
                    return num1 / num2;
                case '%':
                    return num1 % num2;
            }
            return -1;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (sb.Length >= 0 && sb.Length <= 20)
            {
                this.textBox1.Font = new System.Drawing.Font("굴림", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            }
            else if (sb.Length >= 21 && sb.Length <= 32)
            {
                this.textBox1.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            }
            else if (sb.Length >= 33 && sb.Length <= 53)
            {
                this.textBox1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            }
            else if (sb.Length >= 54 && sb.Length <= 74)
            {
                this.textBox1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            }
            else if (sb.Length >= 75)
            {
                MessageBox.Show("숫자가 너무 큽니다.");
                sb.Remove(sb.Length - 1, 1);
                textBox1.Text = sb.ToString();
                len--;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void reset_list_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            AllReset();
            numflag = true;                                     // 선택된 식은 무조건 오퍼레이터가 포함되어있으므로
            opflag = true;
            comma_flag= true;
            string a = listBox1.Text;                           // 선택된 식을 불러온다.
            string[] b = new string[256];                       // 식을 스플릿 할 배열
            b = a.Split('=');

            textBox1.Text = b[0];
            a = b[0];                                           //다시 덮어쓰기 

            for (int i = 0; i < a.Length; i++)
            {
                sb.Append(a[i]);
            }
            if (sb[sb.Length-1] != 0 )
            zeroflag = true;


            //listBox1.Text.Remove(listBox1.Text.Length - 1, 1);
            //textBox1.Text = listBox1.Text;
        }

        private void BtnOn()
        {
            btReset.Enabled = true;
            bt0.Enabled = true;
            bt1.Enabled = true;
            bt2.Enabled = true;
            bt3.Enabled = true;
            bt4.Enabled = true;
            bt5.Enabled = true;
            bt6.Enabled = true;
            bt7.Enabled = true;
            bt8.Enabled = true;
            bt9.Enabled = true;

            btAdd.Enabled = true;
            btSub.Enabled = true;
            btMul.Enabled = true;
            btDiv.Enabled = true;
            btMod.Enabled = true;
            btBracket1.Enabled = true;
            btBracket2.Enabled = true;
            btComma.Enabled = true;
            btEqual.Enabled = true;
            btEqual.BackColor = Color.RosyBrown;
            del.Enabled = true;
            listBox1.Enabled = true;
            listBox1.BackColor = Color.White;
            listBox2.Enabled = true;
            listBox2.BackColor = Color.White;
            textBox1.BackColor = Color.Beige;
            bt_power.Text = "끄기";
            reset_list.Enabled = true;
            power = false;                                                  // 파워가 켜져있고 꺼질수 있음
        }
        private void BtnOff()
        {
            btReset.Enabled = false;

            bt0.Enabled = false;
            bt1.Enabled = false;
            bt2.Enabled = false;
            bt3.Enabled = false;
            bt4.Enabled = false;
            bt5.Enabled = false;
            bt6.Enabled = false;
            bt7.Enabled = false;
            bt8.Enabled = false;
            bt9.Enabled = false;

            btAdd.Enabled = false;
            btSub.Enabled = false;
            btMul.Enabled = false;
            btDiv.Enabled = false;
            btMod.Enabled = false;
            btBracket1.Enabled = false;
            btBracket2.Enabled = false;
            btComma.Enabled = false;
            btEqual.Enabled = false;
            btEqual.BackColor = Color.WhiteSmoke;
            del.Enabled = false;
            listBox1.Enabled = false;
            listBox1.BackColor = Color.Black;
            listBox2.Enabled = false;
            listBox2.BackColor = Color.Black;
            reset_list.Enabled = false;
            textBox1.BackColor = Color.Gray;
            AllReset();
            bt_power.Text = "켜기";
            power = true;
        }
        private void BtnOff_Negative()
        {
            bt0.Enabled = false;
            bt1.Enabled = false;
            bt2.Enabled = false;
            bt3.Enabled = false;
            bt4.Enabled = false;
            bt5.Enabled = false;
            bt6.Enabled = false;
            bt7.Enabled = false;
            bt8.Enabled = false;
            bt9.Enabled = false;
            btAdd.Enabled = false;
            btSub.Enabled = false;
            btMul.Enabled = false;
            btDiv.Enabled = false;
            btMod.Enabled = false;
            btBracket1.Enabled = false;
            btBracket2.Enabled = false;
            btComma.Enabled = false;
            btEqual.Enabled = false;
            del.Enabled = false;
            listBox1.Enabled = false;
            listBox1.BackColor = Color.Black;
            listBox2.Enabled = false;
            listBox2.BackColor = Color.Black;
            MessageBox.Show("연산 결과, 음수 혹은 수가 아닌 값이 나왔으므로 해당 값으로 계산 진행이 불가합니다\n C(리셋)버튼을 눌러 주세요.", "알림");
        }
        private void bt_power_Click(object sender, EventArgs e)
        {
            if (!power)                                                     // 파워가 켜져있을 때
            {
                if (!firstExe) { firstExe = true; }                         // 처음 실행할 때 (1) 한번 더 묻는 창 안뜸
                else
                {
                    if (MessageBox.Show("전원을 끄시겠습니까?", "Good Bye?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {   //yes를 눌렀을 때
                        BtnOff();
                        MessageBox.Show("Made by Hosugi ^0^", "Thank you!");
                    }
                    else { return; }                                        //no를 눌렀을 때                                                  
                }
                //(1) 여기로 바로옴
                BtnOff();
            }
            else
            {
                BtnOn();
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)               // 버튼 말고 키보드를 눌렀을 때
        {
            KeyCheck(ref sender, ref e);
        }
    }
}
