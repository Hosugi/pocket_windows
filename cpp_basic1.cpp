#include <iostream>

using namespace std;
float c_to_f(int);
int main()
{	
	float c,f;
	cout << "섭시 온도를 입력하고 Enter 키를 누르세요 : ";
	cin >> c;
	cout << "섭씨 " << c << "도는 화씨로 " << c_to_f(c) << "도 입니다.";
	return 0;
}

float c_to_f(int c) {
	return c * 1.8 + 32;
}