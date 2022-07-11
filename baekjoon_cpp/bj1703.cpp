#include <iostream>
#include <cstdint>
#include <vector>
using namespace std;
int age = 0;
//vector <int> v;
int main()
{

	while (true) {
	int stick = 0 , leaf=1, cut=0;
		cin >> age;
		if (!age) return 0;
		for (int i=0; i< age; i++){
			cin >> stick >> cut;
			leaf = stick * leaf - cut;
		}
		cout << leaf << endl;
	}

	return 0;
}