#include <iostream>
#include <vector>
#include<cmath>
#include <iomanip>
using namespace std;

typedef long long longBoi;
class MaxHeap
{
private:
	vector<double> heapArray;
	double* myArray = new double[500000];
	longBoi capacity;
	longBoi size = 0;

	longBoi parent(int i) { return((i - 1) / 2); }
	longBoi leftChild(int i) { return (2 * i + 1); }
	longBoi rightChild(int i) { return (2 * i + 2); }

	void swap(double& A, double& B)
	{
		double temp = A;
		A = B;
		B = temp;
	}


	void siftUp(double pos)
	{
		longBoi parentIndex = parent(pos);

		while (heapArray[pos] > heapArray[parentIndex])
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

	void siftDown(double pos)
	{
		longBoi leftChildIndex = leftChild(pos);
		longBoi rightChildIndex = rightChild(pos);

		bool hasLeft = leftChildIndex < heapArray.size();
		bool hasRight = rightChildIndex < heapArray.size();


		if (hasRight && (heapArray[pos] < heapArray[leftChildIndex] || heapArray[pos] < heapArray[rightChildIndex]))
		{
			longBoi swapWith = -1;
			if (heapArray[leftChildIndex] < heapArray[rightChildIndex])
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
		else if (hasLeft && heapArray[pos] < heapArray[leftChildIndex])
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
	void insert(double value)
	{

		heapArray.push_back(value);
		if (heapArray.size() != 0)
		{
			siftUp(heapArray.size() - 1);
		}

	}
	int getMax()
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
class MinHeap
{
private:
	vector<double> heapArray;
	double* myArray = new double[500000];
	longBoi capacity;
	longBoi size = 0;

	longBoi parent(longBoi i) { return((i - 1) / 2); }
	longBoi leftChild(longBoi i) { return (2 * i + 1); }
	longBoi rightChild(longBoi i) { return (2 * i + 2); }

	void swap(double& A, double& B)
	{
		double temp = A;
		A = B;
		B = temp;
	}


	void siftUp(double pos)
	{
		longBoi parentIndex = parent(pos);

		while (heapArray[pos] < heapArray[parentIndex])
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

	void siftDown(double pos)
	{
		longBoi leftChildIndex = leftChild(pos);
		longBoi rightChildIndex = rightChild(pos);

		bool hasLeft = leftChildIndex < heapArray.size();
		bool hasRight = rightChildIndex < heapArray.size();


		if (hasRight && (heapArray[pos] > heapArray[leftChildIndex] || heapArray[pos] > heapArray[rightChildIndex]))
		{
			longBoi swapWith = -1;
			if (heapArray[leftChildIndex] > heapArray[rightChildIndex])
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
		else if (hasLeft && heapArray[pos] > heapArray[leftChildIndex])
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
	void insert(double value)
	{

		heapArray.push_back(value);
		if (heapArray.size() != 0)
		{
			siftUp(heapArray.size() - 1);
		}

	}
	double getMin()
	{
		return heapArray[0];
	}
	void extractMin()
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

int main()
{
	std::ios_base::sync_with_stdio(false);
	std::cin.tie(NULL);
	MinHeap rightHeap;
	MaxHeap leftHeap;

	int numberOfCitizens;
	cin >> numberOfCitizens;
	if( numberOfCitizens == 0)
	{
		cout << 0.0;
		return 0;
	}
	vector<double> results;
	double input;
	double median = 0;
	for (int i = 0; i < numberOfCitizens; ++i)
	{
		cin >> input;

		if (input > median)
		{
			rightHeap.insert(input);
		}
		if (input < median)
		{
			leftHeap.insert(input);
		}

		if(input == median)
		{
			leftHeap.insert(input);
		}

		int leftSize = leftHeap.heapSize();
		int rightSize = rightHeap.heapSize();

		if (abs(rightSize - leftSize) > 1)
		{
			if (rightSize > leftSize)
			{
				leftHeap.insert(rightHeap.getMin());
				rightHeap.extractMin();
			}
			else
			{
				rightHeap.insert(leftHeap.getMax());
				leftHeap.extractMax();
			}
		}

		leftSize = leftHeap.heapSize();
		rightSize = rightHeap.heapSize();

		if(leftSize == rightSize)
		{
			median = (leftHeap.getMax() + rightHeap.getMin()) / 2;
		}
		else
		{

			if (leftSize > rightSize)
			{
				median = leftHeap.getMax();
			}
			else
			{
				median = rightHeap.getMin();
			}
		}


		cout << fixed << setprecision(1) << median << endl;
	}
}

