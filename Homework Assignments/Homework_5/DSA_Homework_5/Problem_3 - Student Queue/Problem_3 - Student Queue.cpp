#include <iostream>
#include <queue>
using namespace std;
typedef  long long longBoi;
struct student
{
	student(string nameInput = " ", longBoi groupInput = 1)
	{
		name = nameInput;
		group = groupInput;
	}
	string name;
	longBoi group;
	longBoi timeOfEntry;
	longBoi timeOfExit;

};
int main()
{
	longBoi numberOfStudents;
	longBoi numberOfGroups;

	cin >> numberOfStudents >> numberOfGroups;

	vector<queue<student>> studentGroups;
	for (longBoi i = 0; i < numberOfGroups; ++i)
	{
		queue<student> newQ;
		studentGroups.push_back(newQ);
	}
	queue<student> inputQueue;

	string nameInput;
	longBoi groupNumber;
	for (longBoi i = 0; i < numberOfStudents; ++i)
	{
		cin >> nameInput;
		cin >> groupNumber;
		student currentStudent(nameInput, groupNumber);
		inputQueue.push(currentStudent);
	}

	queue<longBoi> mainQueue;

	longBoi minuteCounter = 0;
	student firstInLine = inputQueue.front();
	firstInLine.timeOfEntry = minuteCounter;
	inputQueue.pop();
	studentGroups[firstInLine.group-1].push(firstInLine);
	mainQueue.push(firstInLine.group-1);
	bool isGroupEmpty = true;
	longBoi numberOfStudentsPassed = 0;
	if(numberOfStudents==1)
	{
		cout << firstInLine.name << " " << firstInLine.timeOfEntry << " " << 2;
	}
	while (numberOfStudentsPassed < numberOfStudents)
	{
		minuteCounter++;
		if (!inputQueue.empty())
		{
			student nextInLine = inputQueue.front();
			inputQueue.pop();
			nextInLine.timeOfEntry = minuteCounter;
			isGroupEmpty = studentGroups[nextInLine.group - 1].empty();
			studentGroups[nextInLine.group - 1].push(nextInLine);

			if (isGroupEmpty)
			{
				mainQueue.push(nextInLine.group - 1);
			}
		}
		if (minuteCounter % 2 == 0)
		{
			student studentToLeave = studentGroups[mainQueue.front()].front();
			cout << studentToLeave.name << " " << studentToLeave.timeOfEntry << " " << minuteCounter << endl;
			studentGroups[mainQueue.front()].pop();
			if (studentGroups[mainQueue.front()].empty())
			{
				mainQueue.pop();
			}
			numberOfStudentsPassed++;
		}
	}
}
