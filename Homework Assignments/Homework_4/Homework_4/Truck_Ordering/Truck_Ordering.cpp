#include <iostream>
#include <stack>
#include <vector>
typedef long long longBoi;
using namespace std;

struct testCase
{
	longBoi numberOfTrucks;
	longBoi* truckNumbers;
};
int main()
{

	longBoi numberOfCases;
	cin >> numberOfCases;

	testCase* cases = new testCase[numberOfCases];

	longBoi numberOfTrucksPerCase;
	longBoi truckNumber;
	for (longBoi i = 0; i < numberOfCases; ++i)
	{
		cin >> numberOfTrucksPerCase;
		cases[i].numberOfTrucks = numberOfTrucksPerCase;
		cases[i].truckNumbers = new longBoi[numberOfTrucksPerCase];

		for (longBoi j = 0; j < numberOfTrucksPerCase; ++j)
		{
			cin >> truckNumber;
			cases[i].truckNumbers[j] = truckNumber;
		}
	}

#pragma  region  Input verification
	///Input verification test
	/*cout << "Number of cases: " << numberOfCases << endl;
	for (int i = 0; i < numberOfCases; ++i)
	{
		cout << cases[i].numberOfTrucks << endl;
		for (int j = 0; j < cases[i].numberOfTrucks; ++j)
		{
			cout << cases[i].truckNumbers[j] << " ";
		}
		cout << endl;
	}*/
#pragma endregion 

	stack<longBoi> street;
	longBoi lastPushed = -1;
	longBoi rightElement = 0;
	longBoi counter = 1;
	bool continueCheck = true;
	for (longBoi i = 0; i < numberOfCases; ++i)
	{
		for (longBoi j = 0; j < cases[i].numberOfTrucks && continueCheck; j++)
		{
			if(cases[i].truckNumbers[j]!= counter)
			{
				if (street.empty())
				{

					street.push(cases[i].truckNumbers[j]);
					lastPushed = cases[i].truckNumbers[j];
				}
				else if(lastPushed == counter)
				{
					street.pop();
					counter++;
				}
				else if(cases[i].truckNumbers[j] < lastPushed)
				{
					street.push(cases[i].truckNumbers[j]);
					lastPushed =cases[i].truckNumbers[j];
				}
				else
				{
					//Return that you cant sort this thing
					cout << "no" << endl;
					continueCheck = false;
				}
			}
			else if(cases[i].truckNumbers[j] == counter)
			{
				rightElement = cases[i].truckNumbers[j];
				counter++;
			}
		}
		if(continueCheck)
		{
			//Return that you can sort it.
			cout << "yes" << endl;
		}
		counter = 1;
		rightElement = 0;
		lastPushed = -1;
		continueCheck = true;
	}
	return 0;
}
