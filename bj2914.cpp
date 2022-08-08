#include <iostream>
#include <string.h>
using namespace std;
void copyright()
{	
	int a,b,sum;
	cin >> a >> b;
	sum = ((a * b) - a) + 1;
	cout << sum;
}

int main() 
{
	copyright();
	return 0;
}