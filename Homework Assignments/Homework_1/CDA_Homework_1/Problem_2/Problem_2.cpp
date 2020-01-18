// Problem_2.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

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

	std::string line;
	int number;
	std::vector<int> numbers;

	std::getline(std::cin, line);
	std::istringstream stream(line);
	while (stream >> number)
		numbers.push_back(number);

	float passing_grade = numbers[0];	
	
	float availableSum = 0;
	int peopleWhoCanGive = 0;
	float neededSum = 0;

	for (int i = 2; i < numbers.size(); ++i)
	{
		if(numbers[i] > passing_grade)
		{
			availableSum += numbers[i];
			peopleWhoCanGive++;
		}
		else if(numbers[i] < passing_grade)
		{			
			neededSum += (passing_grade -numbers[i]);
		}
	}

	if (peopleWhoCanGive != 0)
	{

		if ((availableSum - neededSum) / peopleWhoCanGive >= passing_grade)
		{
			cout << "yes";
			return 0;
		}
	}
	cout << "no";
	return 0;

}
