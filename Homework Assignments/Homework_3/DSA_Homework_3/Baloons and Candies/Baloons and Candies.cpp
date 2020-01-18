#include <iostream>
#include <algorithm>
#include <cmath>
using namespace  std;
typedef  long long longBoi;

void merge(longBoi* arr, longBoi* helper, longBoi start, longBoi mid, longBoi end)
{

	longBoi left1 = start;
	longBoi left2 = mid;
	longBoi i = start;

	for (; left1 < mid && left2 < end; ++i)

	{
		if (arr[left1] <= arr[left2])
		{
			helper[i] = arr[left1++];
		}
		else
		{
			helper[i] = arr[left2++];
		}
	}

	while (left1 < mid)
	{
		helper[i++] = arr[left1++];
	}

	while (left2 < end)
	{
		helper[i++] = arr[left2++];
	}

	for (longBoi j = start; j < end; ++j)
	{
		arr[j] = helper[j];
	}

}

void merge_sort(longBoi* arr, longBoi* helper, longBoi leftLim, longBoi rightLim)
{
	if (leftLim + 1 < rightLim)
	{

		longBoi middle = (leftLim + rightLim) / 2;
		merge_sort(arr, helper, leftLim, middle);
		merge_sort(arr, helper, middle, rightLim);
		merge(arr, helper, leftLim, middle, rightLim);
	}

}

int main()
{
	longBoi numberOfDays;
	longBoi numberOfBaloons;

	cin >> numberOfDays;
	cin >> numberOfBaloons;

	longBoi* baloonsPerDay = new longBoi[numberOfDays];
	longBoi* numberOfCanidesForBaloon = new longBoi[numberOfDays];

	longBoi input;
	for (longBoi i = 0; i < numberOfDays; ++i)
	{

		cin >> input;
		baloonsPerDay[i] = input;
	}

	for (longBoi i = 0; i < numberOfDays; ++i)
	{
		cin >> input;
		numberOfCanidesForBaloon[i] = input;
	}

	longBoi* worstCaseForCandies = new longBoi[numberOfDays];
	for (longBoi i = 0; i < numberOfDays; ++i)
	{
		worstCaseForCandies[i] = baloonsPerDay[i] * numberOfCanidesForBaloon[i];
	}

	longBoi* helper = new longBoi[numberOfDays];
	merge_sort(worstCaseForCandies, helper, 0, numberOfDays);

	longBoi leftLim = 0;
	longBoi rightLim = worstCaseForCandies[numberOfDays - 1];

	while (leftLim < rightLim)
	{
		longBoi middle = (leftLim + rightLim) / 2;
		longBoi numberOfBalloonsNeeded = 0;

		for (longBoi i = 0; i < numberOfDays; ++i)
		{
			if (numberOfCanidesForBaloon[i] != 0)
			{
				numberOfBalloonsNeeded += max((baloonsPerDay[i] - middle / numberOfCanidesForBaloon[i]), (longBoi)0);
			}
		}

		if (numberOfBalloonsNeeded <= numberOfBaloons)
		{
			rightLim = middle;
		}
		else
		{
			leftLim = middle + 1;
		}
	}

	cout << leftLim;

}

