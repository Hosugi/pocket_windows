#include <iostream>
#include <string.h>
using namespace std;
void cmd()
{
	int loop;
	char main_str[51], sub_str[51];
	cin >> loop;

	//파일 이름의 길이는 모두 같다.
	cin >> main_str;
	for (int i = 0; i < loop - 1; i++)
	{
		int count;
		count = strlen(main_str);					// 메인 배열의 길이

		cin >> sub_str;
		for (int k = 0; k < count; k++)
		{
			if (main_str[k] != sub_str[k])		// main sub 비교
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