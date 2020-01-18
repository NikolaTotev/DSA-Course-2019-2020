#include <iostream>
#include <sstream>
#include <vector>
#include <cmath>
#include <cstdio>
#include <vector>
#include <iostream>
#include <algorithm>
using namespace std;

int main()
{
	int numberOfWalls = 0;
	cin >> numberOfWalls;
	ws(cin);
	std::string line;
	int number;
	std::vector<int> numbers;

	std::getline(std::cin, line);
	std::istringstream stream(line);
	while (stream >> number)
		numbers.push_back(number);

	int max_vol = 0;

	for (int i = 0; i < numbers.size(); ++i)
	{
		for (int j = i; j < numbers.size(); ++j)
		{
			int newMax = min(numbers[j], numbers[i]) * (j-i);
			if(newMax > max_vol)
			{
				max_vol = newMax;
			}
		}
	}
	cout << max_vol;
}


