#include <iostream>
#include <string.h>
using namespace std;
void cmd()
{
	int loop;
	char main_str[51], sub_str[51];
	cin >> loop;

	//���� �̸��� ���̴� ��� ����.
	cin >> main_str;
	for (int i = 0; i < loop - 1; i++)
	{
		int count;
		count = strlen(main_str);					// ���� �迭�� ����

		cin >> sub_str;
		for (int k = 0; k < count; k++)
		{
			if (main_str[k] != sub_str[k])		// main sub ��
			{
				main_str[k] = '?';
			}
		}
	}
	cout << main_str;

}

int main()
{
	cmd();
	return 0;
}