#include <iostream>
#include <string.h>
using namespace std;

void cycle()
{
	int main, ten, one, total;			// one = A ten = B   A + B 
	int count=0;
	int origin;

	cin >> main;
	origin = main;

	do 
	{
		one = main % 10;				// main 에서 10의자리 수 빼기 26 = 6
		if (main < 10 && main >= 0)
		{
			ten = 0;
		}
		else ten = (main - one) / 10; // two 에 ten 만큼 뺀 값 넣기 26 - 6 /10 = 2
		total = one + ten;
		main = (one*10) + total % 10;		// main = 60 + (6 + 2) = 68 6 + 8 = 14 8 + 4 = 12
		count++;
	} while (origin != main);
	cout << count;

}
	// main = 1     0 + 1 = 1 11   1 + 1 = 2 12   1 + 2 = 3 23   2 + 3 = 5 35   3 + 5 = 8 58   5 + 8 = 13 83   
int main()
{
	cycle();
	return 0;
}