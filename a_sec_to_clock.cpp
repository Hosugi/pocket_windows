#include <iostream>
using namespace std;
enum {
	DAY = 3600*24,
	HOUR = 3600,
	MIN = 60,
};

int convert(long input, int &a, int &b, int &c, int &d)
{
	while (DAY < input)
	{
		input -= DAY;
		a++;
	}
	while (HOUR < input)
	{
		input -= HOUR;
		b++;
	}
	while (MIN < input)
	{
		input -= MIN;
		c++;
	}
	d = input;
	return a, b, c, d;
}

int main()
{
	long input;
	int a=0, b=0, c=0, d=0;
	cin >> input;

	convert(input, a, b, c, d);

	cout << a << "일, " << b << "시간, " << c << "분, " << d << "초";
	return 0;
}