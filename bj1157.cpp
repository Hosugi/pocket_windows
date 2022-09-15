#include <iostream>
#include <cstdlib>
#include <cstring>
using namespace std;

char word[1000001];
int main()
{
	const int eng_length = 26;
	char say;
	int box[eng_length] = {0, };						// 들어온 알파벳의 위치을 담을 배열
	int max=0;											// 제일 많이 사용된 알파벳의 아스키 값
	cin >> word;
	for (int i = 0; i < strlen(word); i++)
	{
		if (word[i] >= 97) box[word[i] - 97]++;			//97 = 'a' 영문자 위치의 값을 1 증가시켜준다.
		else if (word[i] < 97 && word[i] > 64) box[(word[i] - 65)]++;	// 65 = 'A'
	}
	for (int j = 0; j < eng_length; j++)
	{
		if (box[j] == max) say = '?';				// 가장 많은 알파벳이 여러개일때
		else if (box[j] > max)						// 알파벳 위치의 값이 max보다 클때 
		{
			max = box[j];							// 그 자리의 알파벳이 가장 많이사용된 알파벳
			say = j + 65;							// 출력값 조정하기
		}
	}
	cout << say;
	return 0;
}