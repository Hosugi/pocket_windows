#include <iostream>
#include <climits>

using namespace std;

int main()
{
	int loop, overflow;
	long long input, sum;
	for (int i = 0; i < 3; i++) {
		cin >> loop;
		overflow = 0;
		sum = 0;
		while (loop--) {
			cin >> input;
			if (sum > 0 && input > 0 && sum > LLONG_MAX - input) {
				sum += input;
				overflow++;
			}
			else if (sum < 0 && input < 0 && sum < LLONG_MIN - input) {
				sum += input;
				overflow--;
			}
			else {
				sum += input;
			}
		}
		if (overflow > 0) {
			cout << "+\n";
		}
		else if (overflow == 0) {
			if (sum > 0) cout << "+\n";
			else if (sum < 0) cout << "-\n";
			else cout << "0\n";
		}
		else {
			cout << "-\n";
		}
	}
	return 0;
}