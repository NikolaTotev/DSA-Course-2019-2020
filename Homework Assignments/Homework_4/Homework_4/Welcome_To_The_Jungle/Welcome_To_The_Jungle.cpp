#include <iostream>
#include <stack>
#include <vector>
#include <cmath>
#include <algorithm>
typedef  long long longBoi;
using namespace std;
struct stackInfo
{
	longBoi min;
	longBoi max;
	longBoi count;
	longBoi position;
};
int main()
{
	longBoi numberOfTrees;
	cin >> numberOfTrees;
	longBoi* treeheights = new longBoi[numberOfTrees];

	longBoi input;
	for (int i = 0; i < numberOfTrees; ++i)
	{
		cin >> input;
		treeheights[i] = input;
	}

	stack<longBoi> trees;
	vector<stackInfo*> infoHistory;
	stackInfo* currentInfo = new stackInfo;
	currentInfo->count = 1;
	currentInfo->max = treeheights[numberOfTrees - 1];
	currentInfo->min = treeheights[numberOfTrees - 1];
	currentInfo->position = numberOfTrees - 1;
	infoHistory.push_back(currentInfo);

	for (int i = numberOfTrees - 2; i >= 0; --i)
	{
		longBoi currentTreeHeight = treeheights[i];
		if (currentTreeHeight < currentInfo->min)
		{
			currentInfo->count++;
			currentInfo->position = i;
			currentInfo->min = currentTreeHeight;
			trees.push(currentTreeHeight);

			continue;
		}

		stackInfo* newInfo = new stackInfo;
		while (!trees.empty() && trees.top() <= currentTreeHeight)
		{
			trees.pop();
		}
		trees.push(currentTreeHeight);
		newInfo->min = currentTreeHeight;
		if (currentTreeHeight >= currentInfo->max)
		{
			newInfo->max = currentTreeHeight;
		}
		else
		{
			newInfo->max = currentInfo->max;
		}
		newInfo->position = i;
		newInfo->count = trees.size();
		infoHistory.push_back(newInfo);
		currentInfo = newInfo;


	}

	longBoi indexWithMaxCount = infoHistory[0]->position;
	longBoi maxCount = infoHistory[0]->count;

	for (int i = 0; i < infoHistory.size(); ++i)
	{
		if (infoHistory[i]->count >= maxCount)
		{
			maxCount = infoHistory[i]->count;
			indexWithMaxCount = infoHistory[i]->position;
		}
	}

	
	cout << indexWithMaxCount;

}

