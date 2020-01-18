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
	longBoi duration ()
	{
		return end - start;
	}
};


void merge(activity* arr, activity* helper, int start, int mid, int end)
{

	int left1 = start;
	int left2 = mid;
	int i = start;

	for (; left1 < mid && left2 < end; ++i)

	{
		if (arr[left1].start <= arr[left2].start)
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
	longBoi activityCounter = 0;
	longBoi numberOfActivities = 0;
	cin >> numberOfActivities;
	
	activity* activityList = new activity[numberOfActivities];
	activity* helper = new activity[numberOfActivities];

	longBoi start = 0;
	longBoi end = 0;

	for (int i = 0; i < numberOfActivities; ++i)
	{
		cin >> start;
		cin >> end;
		activityList[i].start = start;
		activityList[i].end = end;
	}
	merge_sort(activityList, helper, 0, numberOfActivities);

	
	for (int i = 0; i < numberOfActivities; ++i)
	{
		cout << activityList[i].start << endl;
		if((activityList[i].duration() +activityList[i].start) <= activityList[i+1].start)
		{
			cout << "Duration = " << activityList[i].duration() + activityList[i].start << " Next start = " << activityList[i + 1].start << endl;
			activityCounter++;
		}
	}



	cout << activityCounter;
}


