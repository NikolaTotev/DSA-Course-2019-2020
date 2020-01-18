#include <iostream>
#include <queue>
using  namespace std;

bool rottenAppleMap[10001][10001];
struct Apple
{
	bool isRotten = false;
	int dayID;
	int coordVertical;
	int coordHorizontal;

	Apple(int vert = 0, int horiz = 0, int day = 0)
	{
		dayID = day;
		coordVertical = vert;
		coordHorizontal = horiz;
	}

};
void print(int numberOfRows, int numberOfColumns)
{
	int counter = 0;
	for (int i = numberOfRows - 1; i >= 0; --i)
	{
		for (int j = 0; j < numberOfColumns; ++j)
		{

			cout << rottenAppleMap[i][j] << " ";
			if(rottenAppleMap[i][j])
			{
				counter++;
			}

		}
		cout << endl;
	}
	cout << "Number of rotten apples = " << counter;

	cout << endl;
	cout << endl;
}
bool isValid(int coordVert, int coordHoriz)
{
	if (coordVert > 10001 || coordVert < 0)
	{
		return false;
	}
	if (coordHoriz > 10001 || coordHoriz < 0)
	{
		return false;
	}
	if (rottenAppleMap[coordVert][coordHoriz])
	{
		return false;
	}
	return true;
}

int calculateRemainingApples(int numberOfRows, int numberOfColumns, int totalApples)
{
	int counter = 0;
	for (int i = numberOfRows - 1; i >= 0; --i)
	{
		for (int j = 0; j < numberOfColumns; ++j)
		{
			if (rottenAppleMap[i][j])
			{
				counter++;
			}

		}
	}

	return counter;
}
int main()
{
	bool shouldIPrint = true;
	int numberOfRows;
	int numberOfColumns;
	int totalDays;
	cin >> numberOfRows;
	cin >> numberOfColumns;
	cin >> totalDays;
	int totalAmountOfApples = numberOfRows * numberOfColumns;

	int coordVertical;
	int coordHorizontal;
	int day = 1;

	Apple firstRotten;
	Apple secondRotten;

	queue<Apple> applesToRot;

	while (cin >> coordVertical >> coordHorizontal)
	{
		int modifiedVert = coordVertical - 1;
		int modifiedHoriz = coordHorizontal - 1;
		if (day == 1)
		{
			rottenAppleMap[modifiedVert][modifiedHoriz] = true;

			firstRotten.coordVertical = modifiedVert;
			firstRotten.coordHorizontal = modifiedHoriz;
			firstRotten.dayID = 1;
			applesToRot.push(firstRotten);
			day++;
			continue;
		}
		rottenAppleMap[modifiedVert][modifiedHoriz] = true;

		secondRotten.coordVertical = modifiedVert;
		secondRotten.coordHorizontal = modifiedHoriz;
		secondRotten.dayID = 1;
		applesToRot.push(secondRotten);
	}

	int currentDay = 0;
	while (!applesToRot.empty() && currentDay <= totalDays)
	{
		Apple currentApple = applesToRot.front();

		if (currentApple.dayID > currentDay)
		{
			currentDay = currentApple.dayID;
		}

		int upChange = currentApple.coordVertical + 1;
		int downChange = currentApple.coordVertical - 1;
		int leftChange = currentApple.coordHorizontal - 1;
		int rightChange = currentApple.coordHorizontal + 1;

		Apple upApple(upChange, currentApple.coordHorizontal, currentApple.dayID + 1);
		Apple downApple(downChange, currentApple.coordHorizontal, currentApple.dayID + 1);
		Apple leftApple(currentApple.coordVertical, leftChange, currentApple.dayID + 1);
		Apple rightApple(currentApple.coordVertical, rightChange, currentApple.dayID + 1);

		if (isValid(upApple.coordVertical, upApple.coordHorizontal))
		{
			rottenAppleMap[upApple.coordVertical][upApple.coordHorizontal] = true;
			upApple.dayID = currentApple.dayID + 1;
			if (currentDay < totalDays)
			{
				applesToRot.push(upApple);
			}

			if (shouldIPrint)
			{
				print(numberOfRows, numberOfColumns);
			}

		}

		if (isValid(downApple.coordVertical, downApple.coordHorizontal))
		{

			rottenAppleMap[downApple.coordVertical][downApple.coordHorizontal] = true;
			if (currentDay < totalDays)
			{
				applesToRot.push(downApple);
			}

			if (shouldIPrint)
			{
				print(numberOfRows, numberOfColumns);
			}
		}

		if (isValid(leftApple.coordVertical, leftApple.coordHorizontal))
		{
			rottenAppleMap[leftApple.coordVertical][leftApple.coordHorizontal] = true;
			if (currentDay < totalDays)
			{
				applesToRot.push(leftApple);
			}

			if (shouldIPrint)
			{
				print(numberOfRows, numberOfColumns);
			}
		}

		if (isValid(rightApple.coordVertical, rightApple.coordHorizontal))
		{
			rottenAppleMap[rightApple.coordVertical][rightApple.coordHorizontal] = true;
			if (currentDay < totalDays)
			{
				applesToRot.push(rightApple);
			}

			if (shouldIPrint)
			{
				print(numberOfRows, numberOfColumns);
			}
		}

		applesToRot.pop();
	}

	cout << totalAmountOfApples - calculateRemainingApples(numberOfRows, numberOfColumns, totalAmountOfApples);
}

