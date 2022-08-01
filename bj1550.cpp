#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string.h>
using namespace std;
int conver(char* source)
{	
	int sum = 0;
	int xx=1;
	int i = strlen(source)-1;
	while (i >= 0)
	{
		if (source[i] >= '0' && source[i] <= '9')
		{
			sum = sum + (source[i] - '0') * xx;
		}
		else if (source[i] >= 'A' && source[i] <= 'F')
		{
			sum = sum + (source[i] - 55) * xx;
		}
		i--;
		xx *= 16;
	}
	return sum;
}

int main() 
{
	char a[7];
	scanf("%s", a);
	printf("%d", conver(a));
	return 0;
}