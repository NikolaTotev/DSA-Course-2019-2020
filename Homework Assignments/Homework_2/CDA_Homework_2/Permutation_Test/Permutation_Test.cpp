//#include <cmath>
//#include <cstdio>
//#include <vector>
//#include <iostream>
//#include <algorithm>
//#include <cctype>
//
//using namespace std;
//int main()
//{
//	std::ios_base::sync_with_stdio(false);
//	std::cin.tie(NULL);
//	string wordOne;
//	string wordTwo;
//	long long wordLenth;
//
//	cin >> wordLenth;
//	cin >> wordOne;
//	cin >> wordTwo;
//
//	//vector<long long> wordArr1;
//	//vector<long long> wordArr2;
//
//	long long wordArr1[26];
//	long long wordArr2[26];
//
//	if (wordLenth == 0)
//	{
//		cout << "yes";
//		return 0;
//	}
//	
//	if (wordOne == wordTwo) {
//		cout << "yes";
//		return 0;
//	}
//
//	for (int i = 0; i < 26; i++)
//	{
//		/*wordArr1.push_back(0);
//		wordArr2.push_back(0);*/
//		wordArr1[i] = 0;
//		wordArr2[i] = 0;
//	}
//
//	for (long long i = 0; i < wordLenth; ++i)
//	{
//		int indexOne = wordOne[i] - 'a';
//		wordArr1[indexOne]++;
//	}
//
//	for (int i = 0; i < wordLenth; ++i)
//	{
//		int indexTwo = wordTwo[i] - 'a';
//		wordArr2[indexTwo]++;
//	}
//
//	for (int i = 0; i < 26; ++i)
//	{
//		if (wordArr1[i] != wordArr2[i])
//		{
//			cout << "no";
//			return 0;
//		}
//	}
//
//	cout << "yes";
//	return 0;
//}
#include <iostream>
#include <sstream>
#include <vector>
#include <cmath>
#include <cstdio>
#include <vector>
#include <iostream>
#include <algorithm>
using namespace std;
typedef long long longBoi;


struct activity
{
	longBoi start = 0;
	longBoi end = 0;
	longBoi activityEnd;

};


void merge(activity* arr, activity* helper, int start, int mid, int end)
{

	int left1 = start;
	int left2 = mid;
	int i = start;

	for (; left1 < mid && left2 < end; ++i)

	{
		if (arr[left1].activityEnd <= arr[left2].activityEnd)
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

	for (int j = start; j < end; ++j)
	{
		arr[j] = helper[j];
	}

}

void merge_sort(activity* arr, activity* helper, int leftLim, int rightLim)
{
	if (leftLim + 1 < rightLim)
	{

		int middle = (leftLim + rightLim) / 2;
		merge_sort(arr, helper, leftLim, middle);
		merge_sort(arr, helper, middle, rightLim);
		merge(arr, helper, leftLim, middle, rightLim);
	}

}


int main()
{
	std::ios_base::sync_with_stdio(false);
	std::cin.tie(NULL);
	longBoi activityCounter = 1;
	longBoi numberOfActivities = 0;
	cin >> numberOfActivities;

	activity* activityList = new activity[numberOfActivities];
	activity* helper = new activity[numberOfActivities];

	longBoi start = 0;
	longBoi end = 0;
	longBoi currentSum = 0;
	longBoi currentHour = 0;
	for (int i = 0; i < numberOfActivities; ++i)
	{
		cin >> start;
		cin >> end;
		activityList[i].start = start;
		activityList[i].activityEnd = start+end;
		
	}
	merge_sort(activityList, helper, 0, numberOfActivities);

	currentHour = activityList[numberOfActivities - 1].activityEnd;
	for (int i = 1; i < numberOfActivities - 1; ++i)
	{
		if(currentHour <= activityList[i].start)
		{
			activityCounter++;
			currentHour = activityList[i].activityEnd;
		}
	}

	cout << activityCounter;
}

