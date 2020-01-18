#include <iostream>
using namespace std;

struct TreeNode
{
	int data;
	TreeNode* leftChild = nullptr;
	TreeNode* rightChild = nullptr;

	TreeNode() = default;

	TreeNode(int _data)
	{
		data = _data;
	}

	~TreeNode()
	{
		delete leftChild;
		delete rightChild;
	}

	//Copy constructor
	TreeNode(TreeNode& rhsNode)
	{
		data = rhsNode.data;
		if (rhsNode.leftChild)
		{
			leftChild = new TreeNode(*rhsNode.leftChild);
		}
		if (rhsNode.rightChild)
		{
			rightChild = new TreeNode(*rhsNode.rightChild);
		}
	}

	TreeNode& operator = (const TreeNode& rhsNode)
	{
		if (this != &rhsNode)
		{
			delete rightChild;
			delete leftChild;

			data = rhsNode.data;

			if (rhsNode.leftChild)
			{
				leftChild = new TreeNode(*rhsNode.leftChild);
			}
			if (rhsNode.rightChild)
			{
				rightChild = new TreeNode(*rhsNode.rightChild);
			}
		}
		return *this;
	}
};

struct BinarySearchTree
{
private:
	TreeNode* root = nullptr;

	bool doesNodeExist(int value, TreeNode* currentNode)
	{
		if (currentNode)
		{
			if (currentNode->data == value)
			{
				return true;
			}
			if (value > currentNode->data)
			{
				return doesNodeExist(value, currentNode->rightChild);
			}
			return doesNodeExist(value, currentNode->leftChild);
		}
		return false;
	}

	TreeNode* insertNewNode(int value, TreeNode* current)
	{
		if (!current)
		{
			return new TreeNode(value);
		}
		if (value < current->data)
		{
			current->leftChild = insertNewNode(value, current->leftChild);
		}
		if (value > current->data)
		{
			current->rightChild = insertNewNode(value, current->rightChild);
		}
		return current;
	}

	TreeNode* removeNode(int value, TreeNode* current)
	{
		if (!current)
		{
			return nullptr;
		}
		if (value < current->data)
		{
			current->leftChild = removeNode(value, current->leftChild);
		}
		if (value > current->data)
		{
			current->rightChild = removeNode(value, current->rightChild);
		}
		if (value == current->data)
		{
			if (!current->leftChild && !current->rightChild)
			{
				free(current);
				return nullptr;
			}
			if (!current->leftChild)
			{
				TreeNode* tempRight = current->rightChild;
				free(current);
				return  tempRight;
			}
			if (!current->rightChild)
			{
				TreeNode* tempLeft = current->leftChild;
				free(current);
				return tempLeft;
			}

			TreeNode* swapWith = current->rightChild;
			while (swapWith->leftChild)
			{
				swapWith = swapWith->leftChild;
			}

			current->data = swapWith->data;
			current->rightChild = removeNode(swapWith->data, current->rightChild);
		}
		return current;
	}

	void printInOrder(TreeNode* current)
	{
		if (current)
		{
			printInOrder(current->leftChild);
			cout << current->data << ", ";
			printInOrder(current->rightChild);
		}
	}

	void calculateDistanceToNode(int value, TreeNode* current, int &counter )
	{
		
		if(!current)
		{
			return;
		}
		if(value > current->data)
		{
			counter++;
			calculateDistanceToNode(value, current->rightChild, counter);
		}
		if(value<current->data)
		{
			counter++;
			calculateDistanceToNode(value, current->leftChild, counter);
		}
		if(value == current->data)
		{
			return;
		}
	}

public:
	BinarySearchTree() = default;
	BinarySearchTree(const BinarySearchTree* rhsTree)
	{
		root = new TreeNode(*rhsTree->root);
	}

	BinarySearchTree& operator=(const BinarySearchTree& rhsTree)
	{
		if (this != &rhsTree)
		{
			delete root;
			root = new TreeNode(*rhsTree.root);
		}
		return *this;
	}

	~BinarySearchTree()
	{
		delete root;
	}

	bool ContainsAddress(int value)
	{
		return doesNodeExist(value, root);
	}

	void Insert(int value)
	{
		if (!doesNodeExist(value, root))
		{
			root = insertNewNode(value, root);
		}
	}

	void Remove(int value) {
		if (doesNodeExist(value, root))
		{
			root = removeNode(value, root);
		}
	}

	int CalculateDistance(int value)
	{
		int counter = 0;
		calculateDistanceToNode(value, root, counter);
		return counter;
	}

	void PrintInOrder()
	{
		printInOrder(root);
		cout << '\n';
	}
};

int main()
{
	int numberOfHouses;
	int numberOfPackages;

	cin >> numberOfHouses;
	cin >> numberOfPackages;

	int* houseAddresses = new int[numberOfHouses];
	int* packageAddresses = new int[numberOfPackages];
	int* distancesToAddresses = new int[numberOfPackages];
	int MaazonOffice = houseAddresses[0];

	int input;
	//Input house addresses;
	for (int i = 0; i < numberOfHouses; ++i)
	{
		cin >> input;
		houseAddresses[i] = input;
	}

	//Input package addresses;
	for (int i = 0; i < numberOfPackages; ++i)
	{
		cin >> input;
		packageAddresses[i] = input;
	}

	//Create town;
	BinarySearchTree LonBinares;	
	for (int i = 0; i < numberOfHouses; ++i)
	{
		LonBinares.Insert(houseAddresses[i]);
	}

	for (int i = 0; i < numberOfPackages; ++i)
	{
		if(!LonBinares.ContainsAddress(packageAddresses[i]))
		{
			distancesToAddresses[i] = -1;
		}
		else
		{
			distancesToAddresses[i] = 0;
		}
	}

	for (int i = 0; i < numberOfPackages; ++i)
	{
		if(distancesToAddresses[i]!= -1)
		{
			distancesToAddresses[i] = LonBinares.CalculateDistance(packageAddresses[i]);
		}
	}


	for (int i = 0; i < numberOfPackages; ++i)
	{
		cout << distancesToAddresses[i] << " ";
	}
	
}
