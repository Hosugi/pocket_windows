#include <iostream>
#include <cstdlib>
#include <cstring>
using namespace std;

char word[1000001];
int main()
{
	const int eng_length = 26;
	char say;
	int box[eng_length] = {0, };						// ���� ���ĺ��� ��ġ�� ���� �迭
	int max=0;											// ���� ���� ���� ���ĺ��� �ƽ�Ű ��
	cin >> word;
	for (int i = 0; i < strlen(word); i++)
	{
		if (word[i] >= 97) box[word[i] - 97]++;			//97 = 'a' ������ ��ġ�� ���� 1 ���������ش�.
		else if (word[i] < 97 && word[i] > 64) box[(word[i] - 65)]++;	// 65 = 'A'
	}
	for (int j = 0; j < eng_length; j++)
	{
		if (box[j] == max) say = '?';				// ���� ���� ���ĺ��� �������϶�
		else if (box[j] > max)						// ���ĺ� ��ġ�� ���� max���� Ŭ�� 
		{
			max = box[j];							// �� �ڸ��� ���ĺ��� ���� ���̻��� ���ĺ�
			say = j + 65;							// ��°� �����ϱ�
		}
	}
	cout << say;
	return 0;
}