#include <iostream>
#include <vector>
#include <set>
using namespace std;
int main()
{
	vector<int> doubles = { 2,4,6,8,10,12, 14,16,18,20,22,24,26,28,30,32,34,36,38,40, 50 };
	vector<int> allPoints = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,24,25,26,27,28,30,32,33,34,36,38,39,40,42,45,48,50,51,54,57,60 };
	set<int> setDoubles = { 2,4,6,8,10,12, 14,16,18,20,22,24,25,26,28,30,32,34,36,38,40, 50 };
	set<int> setInvalid = { 23, 29, 31,35,37,41,43,44,46,47,49,52,53,56,58,59 };

	int neededPoints = 0;
	cin >> neededPoints;

	int numberOfOptions = 0;

	if (neededPoints == 0)
	{
		return 0;
	}

	bool showDebugOutput = true;

	//with single arrow
	for (int i = 0; i < doubles.size(); ++i)
	{
		if (doubles[i] == neededPoints)
		{
			numberOfOptions++;
			if (showDebugOutput)
			{
				cout << doubles[i] / 2 << "-dbl" << endl;
			}
			numberOfOptions++;
			if (showDebugOutput)
			{
				cout << "0, " << doubles[i] / 2 << "-dbl" << endl;
			}
			numberOfOptions++;
			if (showDebugOutput)
			{
				cout << "0, 0, " << doubles[i] / 2 << "-dbl" << endl;
			}
		}
	}

	int leftIndex = 0;
	int rightIndex = doubles.size() - 1;

	//with 2 arrows
	for (int i = 0; i < doubles.size(); ++i)
	{
		int firstArrow = doubles[i];

		for (int j = 0; j < allPoints.size(); ++j)
		{
			if (firstArrow + allPoints[j] == neededPoints)
			{
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << allPoints[j] << ", " << firstArrow / 2 << "-dbl, " << endl;

				}

				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << "0, " << allPoints[j] << ", " << firstArrow / 2 << "-dbl, " << endl;

				}
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << allPoints[j] << ", 0, " << firstArrow / 2 << "-dbl" << endl;

				}

				if (setDoubles.find(allPoints[j]) != setDoubles.end())
				{
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << firstArrow / 2 << "-dbl, " << allPoints[j] / 2 << "-dbl" << endl;
					}
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << "0, " << firstArrow / 2 << "-dbl, " << allPoints[j] / 2 << "-dbl" << endl;
					}
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << firstArrow / 2 << "-dbl, 0, " << allPoints[j] / 2 << "-dbl" << endl;
					}
				}
			}
		}
	}


	//with 3 arrows
	for (int i = 0; i < doubles.size(); ++i)
	{
		//the last arrow must be a double
		int lastArrow = doubles[i];

		for (int j = 0; j < allPoints.size(); ++j)
		{
			int firstArrow = allPoints[j];
			int remainder = neededPoints - lastArrow - firstArrow;
			if (remainder <= 0 || remainder > allPoints[allPoints.size() - 1] || setInvalid.find(remainder) != setInvalid.end())
			{
				break;
			}

			numberOfOptions++;
			if (showDebugOutput)
			{
				cout << firstArrow << ", " << remainder << ", " << lastArrow / 2 << "-dbl" << endl;
			}

			///New
		/*	numberOfOptions++;
			if (showDebugOutput)
			{
				cout << remainder << ", " << firstArrow << ", " << lastArrow / 2 << "-dbl" << endl;
			}*/
			///

			if (setDoubles.find(firstArrow) != setDoubles.end())
			{
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << firstArrow / 2 << "-dbl, " << remainder << ", " << lastArrow / 2 << "-dbl" << endl;
				}
				///New
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << remainder << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;
				}
				///
			}

			if (setDoubles.find(remainder) != setDoubles.end())
			{
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << firstArrow << ", " << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl; ///f r l
				}
				///New
				
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << remainder / 2 << "-dbl, "<<firstArrow << ", "   << lastArrow / 2 << "-dbl" << endl; ///r f l
				}

				
				///
			}
			///When all doubles ================================================================================================================
			if (setDoubles.find(remainder) != setDoubles.end() && setDoubles.find(firstArrow) != setDoubles.end()) 
			{
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << firstArrow / 2 << "-dbl, " << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;  ///f r l;
				}
				///New
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl, " << remainder / 2 << "-dbl" << endl; ///f l r
				}

				numberOfOptions++;
				if (showDebugOutput)
				{
					cout  << remainder / 2 << "-dbl, " << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl; ///r f l
				}

				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl, " << firstArrow / 2 << "-dbl" << endl; ///r l f
				}

				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << lastArrow / 2 << "-dbl, " << firstArrow / 2 << "-dbl, " << remainder / 2 << "-dbl" << endl;///l f r
				}

				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << lastArrow / 2 << "-dbl, " << remainder / 2 << "-dbl, " << firstArrow / 2 << "-dbl" << endl;///l r f
				}
				///
			}
			///==============================================================================================================================

			if (firstArrow != remainder)
			{
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << remainder << ", " << firstArrow << ", " << lastArrow / 2 << "-dbl" << endl;
				}
				///New
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout <<  firstArrow << ", " << remainder << ", " << lastArrow / 2 << "-dbl" << endl;
				}
				///

				if (setDoubles.find(firstArrow) != setDoubles.end())
				{
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << remainder << ", " << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;
					}

					///New
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << firstArrow / 2 << "-dbl, "  << remainder << ", "  << lastArrow / 2 << "-dbl" << endl;
					}
					///
				}

				if (setDoubles.find(remainder) != setDoubles.end())
				{
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << remainder / 2 << "-dbl, " << firstArrow << ", " << lastArrow / 2 << "-dbl" << endl;
					}
					///New
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout  << firstArrow << ", " << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;
					}
					///
				}

				if (setDoubles.find(remainder) != setDoubles.end() && setDoubles.find(firstArrow) != setDoubles.end())
				{
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << remainder / 2 << "-dbl, " << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl; ///r f l
					}
					///New

					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << firstArrow / 2 << "-dbl, " << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;///f r l
					}

					///
				}
			}
		}

	}

	cout << numberOfOptions;
}


/*
 #include <iostream>
#include <vector>
#include <set>
using namespace std;
int main()
{
	vector<int> doubles = { 2,4,6,8,10,12, 14,16,18,20,22,24,26,28,30,32,34,36,38,40, 50 };
	vector<int> allPoints = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,24,25,26,27,28,30,32,33,34,36,38,39,40,42,45,48,50,51,54,57,60 };
	set<int> setDoubles = { 2,4,6,8,10,12, 14,16,18,20,22,24,25,26,28,30,32,34,36,38,40, 50 };
	set<int> setInvalid = { 23, 29, 31,35,37,41,43,44,46,47,49,52,53,56,58,59 };

	int neededPoints = 0;
	cin >> neededPoints;

	int numberOfOptions = 0;

	if (neededPoints == 0)
	{
		return 0;
	}

	bool showDebugOutput = true;

	//with single arrow
	for (int i = 0; i < doubles.size(); ++i)
	{
		if (doubles[i] == neededPoints)
		{
			numberOfOptions++;
			if (showDebugOutput)
			{
				cout << doubles[i] / 2 << "-dbl" << endl;
			}
			numberOfOptions++;
			if (showDebugOutput)
			{
				cout << "0, " << doubles[i] / 2 << "-dbl" << endl;
			}
			numberOfOptions++;
			if (showDebugOutput)
			{
				cout << "0, 0, " << doubles[i] / 2 << "-dbl" << endl;
			}
		}
	}

	int leftIndex = 0;
	int rightIndex = doubles.size() - 1;

	//with 2 arrows
	for (int i = 0; i < doubles.size(); ++i)
	{
		int firstArrow = doubles[i];

		for (int j = 0; j < allPoints.size(); ++j)
		{
			if (firstArrow + allPoints[j] == neededPoints)
			{
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << allPoints[j] << ", " << firstArrow / 2 << "-dbl, " << endl;

				}

				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << "0, " << allPoints[j] << ", " << firstArrow / 2 << "-dbl, " << endl;

				}
				numberOfOptions++;
				if (showDebugOutput)
				{
					cout << allPoints[j] << ", 0, " << firstArrow / 2 << "-dbl" << endl;

				}

				if (setDoubles.find(allPoints[j]) != setDoubles.end())
				{
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << firstArrow / 2 << "-dbl, " << allPoints[j] / 2 << "-dbl" << endl;
					}
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << "0, " << firstArrow / 2 << "-dbl, " << allPoints[j] / 2 << "-dbl" << endl;
					}
					numberOfOptions++;
					if (showDebugOutput)
					{
						cout << firstArrow / 2 << "-dbl, 0, " << allPoints[j] / 2 << "-dbl" << endl;
					}
				}
			}
		}
	}


	//with 3 arrows
	for (int i = 0; i < doubles.size(); ++i)
	{
		//the last arrow must be a double
		int lastArrow = doubles[i];

		for (int j = 0; j < allPoints.size(); ++j)
		{
			int firstArrow = allPoints[j];
			int remainder = neededPoints - lastArrow - firstArrow;
			if (remainder <= 0 || remainder > allPoints[allPoints.size() - 1] || setInvalid.find(remainder) != setInvalid.end())
			{
				break;
			}

			numberOfOptions++;
			if (showDebugOutput)
			{
				cout << firstArrow << ", " << remainder << ", " << lastArrow / 2 << "-dbl" << endl;
			}

			///New
		/*	numberOfOptions++;
			if (showDebugOutput)
			{
				cout << remainder << ", " << firstArrow << ", " << lastArrow / 2 << "-dbl" << endl;
			}
			///

if (setDoubles.find(firstArrow) != setDoubles.end())
{
	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << firstArrow / 2 << "-dbl, " << remainder << ", " << lastArrow / 2 << "-dbl" << endl;
	}
	///New
	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << remainder << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;
	}
	///
}

if (setDoubles.find(remainder) != setDoubles.end())
{
	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << firstArrow << ", " << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl; ///f r l
	}
	///New

	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << remainder / 2 << "-dbl, " << firstArrow << ", " << lastArrow / 2 << "-dbl" << endl; ///r f l
	}


	///
}
///When all doubles ================================================================================================================
if (setDoubles.find(remainder) != setDoubles.end() && setDoubles.find(firstArrow) != setDoubles.end())
{
	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << firstArrow / 2 << "-dbl, " << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;  ///f r l;
	}
	///New
	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl, " << remainder / 2 << "-dbl" << endl; ///f l r
	}

	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << remainder / 2 << "-dbl, " << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl; ///r f l
	}

	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl, " << firstArrow / 2 << "-dbl" << endl; ///r l f
	}

	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << lastArrow / 2 << "-dbl, " << firstArrow / 2 << "-dbl, " << remainder / 2 << "-dbl" << endl;///l f r
	}

	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << lastArrow / 2 << "-dbl, " << remainder / 2 << "-dbl, " << firstArrow / 2 << "-dbl" << endl;///l r f
	}
	///
}
///==============================================================================================================================

if (firstArrow != remainder)
{
	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << remainder << ", " << firstArrow << ", " << lastArrow / 2 << "-dbl" << endl;
	}
	///New
	numberOfOptions++;
	if (showDebugOutput)
	{
		cout << firstArrow << ", " << remainder << ", " << lastArrow / 2 << "-dbl" << endl;
	}
	///

	if (setDoubles.find(firstArrow) != setDoubles.end())
	{
		numberOfOptions++;
		if (showDebugOutput)
		{
			cout << remainder << ", " << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;
		}

		///New
		numberOfOptions++;
		if (showDebugOutput)
		{
			cout << firstArrow / 2 << "-dbl, " << remainder << ", " << lastArrow / 2 << "-dbl" << endl;
		}
		///
	}

	if (setDoubles.find(remainder) != setDoubles.end())
	{
		numberOfOptions++;
		if (showDebugOutput)
		{
			cout << remainder / 2 << "-dbl, " << firstArrow << ", " << lastArrow / 2 << "-dbl" << endl;
		}
		///New
		numberOfOptions++;
		if (showDebugOutput)
		{
			cout << firstArrow << ", " << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;
		}
		///
	}

	if (setDoubles.find(remainder) != setDoubles.end() && setDoubles.find(firstArrow) != setDoubles.end())
	{
		numberOfOptions++;
		if (showDebugOutput)
		{
			cout << remainder / 2 << "-dbl, " << firstArrow / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl; ///r f l
		}
		///New

		numberOfOptions++;
		if (showDebugOutput)
		{
			cout << firstArrow / 2 << "-dbl, " << remainder / 2 << "-dbl, " << lastArrow / 2 << "-dbl" << endl;///f r l
		}

		///
	}
}
		}

	}

	cout << numberOfOptions;
}



 */