#include <iostream>
using namespace std;
void AVG()
{
	double arr[1001];
	int loop, big;
	double avg, total = 0;
	cin >> loop;
	cin >> arr[0];
	big = arr[0];
	for (int i = 1; i < loop; i++)
	{
		cin >> arr[i];
		if (arr[i] > big) big = arr[i];
	}
	for (int j = 0; j < loop; j++)
	{
		arr[j] = (arr[j] / big) * 100;
		total += arr[j];
	}
	avg = total / loop;
	cout << avg;
}

int main()
{
	AVG();
	return 0;

}