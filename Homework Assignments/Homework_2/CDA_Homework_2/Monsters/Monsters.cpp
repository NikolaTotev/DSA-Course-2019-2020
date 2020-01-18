#include <iostream>
#include <vector>
#include <cmath>
#include <cstdio>
#include <algorithm>
using namespace std;


void merge(long long* arr, long long* helper, long long start, long long mid, long long end)
{

	int left1 = start;
	int left2 = mid;
	int i = start;

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

	for (int j = start; j < end; ++j)
	{
		arr[j] = helper[j];
	}

}

void merge_sort(long long* arr, long long* helper, long long leftLim, long long rightLim)
{
	if (leftLim + 1 < rightLim)
	{

		long long middle = (leftLim + rightLim) / 2;
		merge_sort(arr, helper, leftLim, middle);
		merge_sort(arr, helper, middle, rightLim);
		merge(arr, helper, leftLim, middle, rightLim);
	}

}

int main()
{
	std::ios_base::sync_with_stdio(false);
	std::cin.tie(NULL);
	long long monsterCount = 0;
	long long blastPower = 0;

	cin >> monsterCount;

	cin >> blastPower;


	//vector<int> monsters;
	long long* monsters = new long long[monsterCount];

	long long index = 0;
	for (long long i = 0; i < monsterCount; ++i)
	{
		cin >> index;
		//monsters.push_back(index);
		monsters[i] = index;
	}
	long long* helper = new long long[monsterCount];
	merge_sort(monsters, helper, 0, monsterCount);

	long long maxIndex = monsterCount - 1;
	long long max = monsters[maxIndex];
	long long numberOfShots = 0;
	long long blastSum = 0;
	long long maxCounter = 0;
	bool posExists = true;


	
	while((max - blastSum )> 0)
	{
		max = monsters[maxIndex];
		if(monsters[maxIndex - 1]!=max)
		{
			blastSum += blastPower;
			numberOfShots++;
			monsters[maxIndex] = 0;
			maxIndex -= 1;
		}
		else
		{
			monsters[maxIndex] = 0;
			maxIndex--;
		}
	}
	
	std::cout << numberOfShots-1;


}
