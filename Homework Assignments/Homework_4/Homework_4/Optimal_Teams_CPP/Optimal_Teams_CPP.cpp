// Optimal_Teams_CPP.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <vector>
#include <algorithm>
#include <array>
typedef int  longBoi;
using namespace  std;

struct studentInfo {
	longBoi position;
	longBoi numberOfSkills;
};

int main()
{




	longBoi numberOfStudents;
	longBoi selectionRange;
	cin >> numberOfStudents;
	cin >> selectionRange;

	longBoi* studentSkills = new longBoi[numberOfStudents];
	longBoi* sortedSkills = new longBoi[numberOfStudents];



	std::vector<longBoi>::iterator sortedItterator;


	longBoi input;
	for (int i = 0; i < numberOfStudents; ++i)
	{
		cin >> input;
		studentSkills[i] = input;
		sortedSkills[i] = input;
	}

	sort(sortedSkills, sortedSkills + numberOfStudents);

	longBoi* studentInfoPositions = new longBoi[numberOfStudents];
	longBoi* studentInfoValues = new longBoi[numberOfStudents];

	for (int i = 0; i < numberOfStudents; i++)
	{
		studentInfo newInfo;

		longBoi position;
		for (int j = 0; j < numberOfStudents; ++j)
		{
			if (studentSkills[j] == sortedSkills[i])
			{
				position = j;
			}
		}
		newInfo.position = position;
		newInfo.numberOfSkills = studentSkills[i];
		studentInfoPositions[i] = newInfo.position;
		studentInfoValues[i] = newInfo.numberOfSkills;
	}

	bool isIvansTurn = true;

	longBoi teamONEStudentCount = 0;
	longBoi* team1 = new longBoi[numberOfStudents];

	longBoi teamTWOStudentCount = 0;
	longBoi* team2 = new longBoi[numberOfStudents];

	longBoi studentsPicked = 0;
	longBoi currentMax = sortedSkills[numberOfStudents - 1];

	longBoi position;
	for (int j = 0; j < numberOfStudents; ++j)
	{
		if (studentInfoValues[j] == currentMax)
		{
			position = j;
		}
	}
	longBoi currentMaxIndex = position;
	longBoi studentsCurrentlyPicked = 0;
	bool currentMaxChanged = false;




	while (studentsPicked < numberOfStudents)
	{

		bool isContained1 = false;
		for (int j = 0; j < numberOfStudents; ++j)
		{
			if (team1[j] == currentMax)
			{
				isContained1 = true;
			}
		}
		bool isContained2 = false;
		for (int j = 0; j < numberOfStudents; ++j)
		{
			if (team2[j] == currentMax)
			{
				isContained2 = true;
			}
		}
		if (!isContained1 && !isContained2)
		{
			if (!isIvansTurn)
			{
				team2[0] = currentMax;
				teamTWOStudentCount++;
			}
			else
			{
				team1[0] = currentMax;
				teamONEStudentCount++;
			}

			studentsPicked++;
		}

		for (int i = currentMaxIndex + 1; studentsCurrentlyPicked < selectionRange && i < numberOfStudents; i++)
		{
			 isContained1 = false;
			for (int j = 0; j < numberOfStudents; ++j)
			{
				if (team1[j] == studentSkills[i])
				{
					isContained1 = true;
				}
			}
			 isContained2 = false;
			for (int j = 0; j < numberOfStudents; ++j)
			{
				if (team2[j] == studentSkills[i])
				{
					isContained2 = true;
				}
			}
			if (!isContained1 && !isContained2)
			{
				if (!isIvansTurn)
				{
					team2[0] = studentSkills[i];
					teamTWOStudentCount++;
				}
				else
				{
					team1[0] = studentSkills[i];
					teamONEStudentCount++;
				}
				studentsCurrentlyPicked++;

				studentsPicked++;
			}

			studentsCurrentlyPicked = 0;
			for (int i = currentMaxIndex - 1; studentsCurrentlyPicked < selectionRange && i >= 0; i--)
			{
				 isContained1 = false;
				for (int j = 0; j < numberOfStudents; ++j)
				{
					if (team1[j] == studentSkills[i])
					{
						isContained1 = true;
					}
				}
				 isContained2 = false;
				for (int j = 0; j < numberOfStudents; ++j)
				{
					if (team2[j] == studentSkills[i])
					{
						isContained2 = true;
					}
				}
				if (!isContained1 && !isContained2) {
					if (!isIvansTurn)
					{
						team2[0] = studentSkills[i];
						teamTWOStudentCount++;
					}
					else
					{
						team1[0] = studentSkills[i];
						teamONEStudentCount++;
					}
					studentsCurrentlyPicked++;

					studentsPicked++;
				}
				studentsCurrentlyPicked = 0;
				currentMaxChanged = false;
				if (studentsPicked < numberOfStudents || !currentMaxChanged)
				{
					for (int i = numberOfStudents - 1; i >= 0; i--)
					{
						 isContained1 = false;
						for (int j = 0; j < numberOfStudents; ++j)
						{
							if (team1[j] == sortedSkills[i])
							{
								isContained1 = true;
							}
						}
						 isContained2 = false;
						for (int j = 0; j < numberOfStudents; ++j)
						{
							if (team2[j] == sortedSkills[i])
							{
								isContained2 = true;
							}
						}
						if (!isContained1 && !isContained2) 
						{
							if (!currentMaxChanged)
							{
								currentMax = sortedSkills[i];

								currentMaxChanged = true;

								longBoi position;
								for (int j = 0; j < numberOfStudents; ++j)
								{
									if (studentInfoValues[j] == currentMax)
									{
										position = j;
									}
								}

								currentMaxIndex = position;
							}
						}
					}
				}

				isIvansTurn = !isIvansTurn;
			}

			for (int i = 0; i < numberOfStudents; ++i)
			{
				 isContained1 = false;
				for (int j = 0; j < numberOfStudents; ++j)
				{
					if (team1[j] == studentSkills[i])
					{
						isContained1 = true;
					}
				}
				if (!isContained1) {
					cout << "1";
				}
				else {
					cout << "2";
				}
			}

			return 0;
		}
	}
}