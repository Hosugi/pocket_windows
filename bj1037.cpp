#include <iostream>
#include <algorithm>
using namespace std;
int compare(const void* a, const void* b)
{
	int* x = (int*)a;				//Ÿ���� ��� �������.
	int* y = (int*)b;
	if (*x > *y) return 1;
	else if (*x < *y) return -1;
	return 0;
}
int main()
{
	int count, real,i;
	int arr[51];


	cin >> count;
	for (i = 0; i < count; i++)
	{
		cin >> arr[i];
	}
	qsort(arr, count, sizeof(int), compare);			// ���� ����~ū ��
	real = arr[0] * arr[i-1];					//����� ���ҷ��� 2 , 3 ,5 �� �ϳ��� �־��� �� �߿� ���� ū���� ���ϸ� ��¥ ����� ����.
	cout << real;
	return 0;
}