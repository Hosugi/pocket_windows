#include <iostream>
using namespace std;
enum {
	MIN = 60,
	SEC = 60
};
void degree()
{
	int degree, min, sec;
	cout << "위도를 도, 분, 초 단위로 입력하세요 :" << endl;
	cout << "도각: ";
	cin >> degree;
	cout << "분각: ";
	cin >> min;
	cout << "초각: ";
	cin >> sec;

	const double min_a = MIN;
	const double sec_a = SEC;
	double ans = degree + (min / min_a) + (sec / (sec_a*sec_a));
	
	cout << degree << "도 " << min << "분 " << sec << "초 = ";
	cout << ans << "도";
	return;
}
int main()
{
	degree();
	return 0;
}