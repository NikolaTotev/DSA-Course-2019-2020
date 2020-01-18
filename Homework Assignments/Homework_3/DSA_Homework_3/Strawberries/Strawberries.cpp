
#include <iostream>

using namespace std;
typedef long long longBoi;
struct bowl
{
	longBoi bottomLimit = 0;
	longBoi topLimit = 0;
};

int binarySearchPosition(bowl* mainArray, longBoi start, longBoi end, longBoi numberToFind)
{
	while (start <= end)
	{
		longBoi middle = (start + end) / 2;
		if (mainArray[middle].topLimit >= numberToFind && mainArray[middle].bottomLimit  <=numberToFind)
		{
			return middle+1;
		}
		if(mainArray[middle].topLimit < numberToFind)
		{
			start = middle + 1;
		}
		else
		{
			end = middle - 1;
		}
	}
	return 0;
}
int main()
{
	longBoi numberOfBowls =  0;
	cin >> numberOfBowls;
	longBoi* intervals = new longBoi[numberOfBowls];
	longBoi input = 0;
	for (longBoi i = 0; i < numberOfBowls; ++i)
	{
		cin >> input;
		intervals[i] = input;
	}
	bowl* bowls = new bowl[numberOfBowls];
	for (longBoi i = 0; i < numberOfBowls; ++i)
	{
		if(i == 0)
		{
			
			bowls[i].bottomLimit = 1;
			bowls[i].topLimit = intervals[i];
		}
		else
		{
			bowls[i].bottomLimit = bowls[i - 1].topLimit + 1;
			bowls[i].topLimit = bowls[i-1].topLimit + intervals[i];
		}
	}

	longBoi amountOfNumbersToFind = 0;
	cin >> amountOfNumbersToFind;
	longBoi* numbersToFind = new longBoi[amountOfNumbersToFind];
	longBoi* positionsOfFoundNumbers = new longBoi[amountOfNumbersToFind];
	for (longBoi i = 0; i < amountOfNumbersToFind; ++i)
	{
		cin >> input;
		numbersToFind[i] = input;
	}


	for (longBoi i = 0; i < amountOfNumbersToFind; ++i)
	{
		positionsOfFoundNumbers[i] = binarySearchPosition(bowls, 0, numberOfBowls - 1, numbersToFind[i]);
	}


	for (longBoi i = 0; i < amountOfNumbersToFind; ++i)
	{
		cout << positionsOfFoundNumbers[i] << endl;
	}


}

