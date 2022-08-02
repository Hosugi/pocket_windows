#include <iostream>
using namespace std;

void world_population()
{
	long long world=0, korean=0;
	cout << "세계 인구수를 입력하세요: ";
	cin >> world;
	cout << "대한민국의 인구수를 입력하세요: ";
	cin >> korean;
	float total = (float(korean) / float(world) * 100);
	cout << "세계 인구수에서 대한민국이 차지하는 비중은 " << total << "입니다." << endl;
	return;
}

int main()
{
	world_population();
	return 0;
}