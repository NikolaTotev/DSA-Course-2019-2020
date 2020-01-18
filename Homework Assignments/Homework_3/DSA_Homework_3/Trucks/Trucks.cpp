#include <iostream>
typedef long long longBoi;
using namespace std;

struct truckInfo
{
	longBoi offset;
	longBoi numberOfDrinksToMove;
};

int main()
{
	longBoi numberOfTrucks;
	cin >> numberOfTrucks;



	truckInfo* info = new truckInfo[numberOfTrucks];
	truckInfo* helper = new truckInfo[numberOfTrucks];

	if (numberOfTrucks == 0 || numberOfTrucks == 1)
	{
		cout << 0;
		return 0;
	}
	longBoi offset = 0;
	longBoi numberOfDrinks;
	longBoi numberOfDrinksM1 = 0;
	longBoi numberOfDrinksM2 = 0;

	for (longBoi i = 0; i < numberOfTrucks; ++i)
	{
		cin >> offset;
		cin >> numberOfDrinks;
		info[i].offset = offset;
		info[i].numberOfDrinksToMove = numberOfDrinks;
	}


	longBoi maxOffset = info[0].offset;
	longBoi minOffset = info[0].offset;

	for (longBoi i = 1; i < numberOfTrucks; ++i)
	{
		if (info[i].offset > maxOffset)
		{
			maxOffset = info[i].offset;
		}
		if (info[i].offset < minOffset)
		{
			minOffset = info[i].offset;
		}
	}

	longBoi left = minOffset;
	longBoi right = maxOffset;
	longBoi currentMin = -1;

	while (left <= right)
	{
		longBoi middle1 = left + (right - left) / 3;
		longBoi middle2 = right - (right - left) / 3;

		for (longBoi i = 0; i < numberOfTrucks; ++i)
		{
			numberOfDrinksM1 += abs((info[i].offset - middle1) * info[i].numberOfDrinksToMove);
			numberOfDrinksM2 += abs((info[i].offset - middle2) * info[i].numberOfDrinksToMove);
		}

		if (numberOfDrinksM1 < numberOfDrinksM2)
		{
			right = middle2 - 1;

			if (numberOfDrinksM1 < currentMin || currentMin == -1)
			{
				currentMin = numberOfDrinksM1;
			}
		}
		else if (numberOfDrinksM1 > numberOfDrinksM2)
		{
			left = middle1 + 1;
			if (numberOfDrinksM2 < currentMin || currentMin == -1)
			{
				currentMin = numberOfDrinksM2;
			}
		}
		else
		{
			left = middle1 + 1;
			right = middle2 - 1;
			if (currentMin > numberOfDrinksM2)
			{
				currentMin = numberOfDrinksM2;
			}
		}
		numberOfDrinksM1 = 0;
		numberOfDrinksM2 = 0;
	}
	cout << currentMin;
}
