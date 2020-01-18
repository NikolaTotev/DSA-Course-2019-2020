#include <iostream>
#include <cmath>
#include <vector>
#include <queue>
using namespace std;
typedef long long longBoi;
struct Apartment
{
	double coordX;
	double coordY;
	double distToFMI;
	Apartment()
	{
		coordX = 0;
		coordY = 0;
		distToFMI = 0;
	}
	Apartment& operator=(const Apartment& rhs)
	{
		if (this != &rhs)
		{
			coordX = rhs.coordX;
			coordY = rhs.coordY;
			distToFMI = rhs.distToFMI;
		}
		return *this;
	}

	bool operator < (Apartment& rhs) const
	{
		if (distToFMI == rhs.distToFMI)
		{
			if (coordX == rhs.coordX)
			{
				return false;
			}
			return coordX < rhs.coordX;
		}
		else
		{
			return distToFMI < rhs.distToFMI;
		}
	}

	bool operator <= (Apartment& rhs) const
	{
		return distToFMI <= rhs.distToFMI && coordX < rhs.coordX;
	}

	bool operator >= (Apartment& rhs) const
	{
		return distToFMI >= rhs.distToFMI && coordX > rhs.coordX;
	}
	Apartment(double x, double y, double dist)
	{
		coordX = x;
		coordY = y;
		distToFMI = dist;
	}

};


class MaxHeap
{
private:
	std::vector<Apartment> heapArray;
	longBoi capacity;
	longBoi size = 0;

	int parent(longBoi i) { return((i - 1) / 2); }
	int leftChild(longBoi i) { return (2 * i) + 1; }
	int rightChild(longBoi i) { return (2 * i) + 2; }

	void swap(Apartment& A, Apartment& B)
	{
		Apartment temp = A;
		A = B;
		B = temp;
	}


	void siftUp(longBoi pos)
	{
		longBoi parentIndex = parent(pos);

		while (heapArray[pos].distToFMI >= heapArray[parentIndex].distToFMI)
		{

			swap(heapArray[pos], heapArray[parentIndex]);
			if (parentIndex <= 0)
			{
				return;
			}
			parentIndex = parent(parentIndex);
			pos = parent(pos);
		}


	}

	void siftDown(longBoi pos)
	{
		longBoi leftChildIndex = leftChild(pos);
		longBoi rightChildIndex = rightChild(pos);

		bool hasLeft = leftChildIndex < heapArray.size();
		bool hasRight = rightChildIndex < heapArray.size();


		if (hasRight && (heapArray[pos] <= heapArray[leftChildIndex]
			|| heapArray[pos].distToFMI <= heapArray[rightChildIndex].distToFMI))
		{
			longBoi swapWith = -1;
			if (heapArray[leftChildIndex].distToFMI <= heapArray[rightChildIndex].distToFMI)
			{
				swapWith = rightChildIndex;
			}
			else
			{
				swapWith = leftChildIndex;
			}

			swap(heapArray[pos], heapArray[swapWith]);
			siftDown(swapWith);
		}
		else if (hasLeft && heapArray[pos].distToFMI <= heapArray[leftChildIndex].distToFMI)
		{
			swap(heapArray[pos], heapArray[leftChildIndex]);
			siftDown(leftChildIndex);
		}
	}

public:
	bool isEmpty()
	{
		return heapArray.size() == 0;
	}
	longBoi heapSize()
	{
		return heapArray.size();
	}
	void insert(Apartment value)
	{

		heapArray.push_back(value);
		if (heapArray.size() != 0)
		{
			siftUp(heapArray.size() - 1);
		}

	}
	Apartment getMax()
	{
		return heapArray[0];
	}
	void extractMax()
	{
		if (isEmpty())
		{
			return;
		}
		swap(heapArray[0], heapArray[heapArray.size() - 1]);
		heapArray.pop_back();
		siftDown(0);
	}
};
struct CustomCompare
{
	bool operator () (Apartment& lhs, Apartment& rhs)
	{
		return lhs < rhs;
	}

};
double calculateDistance(double coordX, double coordY)
{
	return sqrt(coordX * coordX + coordY * coordY);
}
int main()
{
	std::ios_base::sync_with_stdio(false);
	std::cin.tie(NULL);
	MaxHeap maxHeap;
	priority_queue<Apartment, vector<Apartment>, CustomCompare>  MHeap;
	longBoi numberOfApartments;
	longBoi apartmentsToFind;
	cin >> numberOfApartments;
	cin >> apartmentsToFind;
	//scanf("%d", &numberOfApartments);
	//scanf("%d", &apartmentsToFind);


	if (numberOfApartments == 0 || apartmentsToFind == 0)
	{
		return 0;
	}


	longBoi inputX;
	longBoi inputY;
	for (longBoi i = 0; i < numberOfApartments; ++i)
	{
		cin >> inputX;
		cin >> inputY;
		//scanf("%lld %lld", &inputX, &inputY);
		double dist = calculateDistance(inputX, inputY);
		Apartment pointToInsert(inputX, inputY, dist);
		/*if (maxHeap.heapSize() == apartmentsToFind)
		{
			if (maxHeap.getMax().distToFMI > dist)
			{
				maxHeap.extractMax();
				maxHeap.insert(pointToInsert);
			}
		}
		else
		{
			maxHeap.insert(pointToInsert);
		}*/
		if (MHeap.size() == apartmentsToFind)
		{
			if (MHeap.top().distToFMI > dist)
			{
				MHeap.pop();
				MHeap.push(pointToInsert);
			}
		}
		else
		{
			MHeap.push(pointToInsert);
		}
	}
	/*
		vector<Apartment> finalApartments;
		while(!maxHeap.isEmpty())
		{
			finalApartments.push_back(maxHeap.getMax());
			maxHeap.extractMax();
		}*/
	vector<Apartment> finalApartments;
	while (!MHeap.empty())
	{
		//cout << MHeap.top().coordX << " " << MHeap.top().coordY << endl;
		finalApartments.push_back(MHeap.top());
		MHeap.pop();
	}

	for (longBoi i = finalApartments.size() - 1; i >= 0; --i)
	{
		cout << finalApartments[i].coordX << " " << finalApartments[i].coordY << endl;
	}
}


