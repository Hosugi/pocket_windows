#include <iostream>
#include <algorithm>
using namespace std;
int compare(const void* a, const void* b)
{
	int* x = (int*)a;				//타입을 명시 해줘야함.
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
	qsort(arr, count, sizeof(int), compare);			// 정렬 작은~큰 순
	real = arr[0] * arr[i-1];					//약수를 구할려면 2 , 3 ,5 중 하나와 주어진 수 중에 제일 큰수와 곱하면 진짜 약수가 나옴.
	cout << real;
	return 0;
}