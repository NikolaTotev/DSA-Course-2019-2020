using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_2_TreeOps
{

    public class TreeNode
    {
        public TreeNode right;
        public TreeNode left;
        public int data;
        public bool isOddLevel;
        public TreeNode()
        {
            right = null;
            left = null;
        }
        public TreeNode(TreeNode rightC, TreeNode leftC, int dataC)
        {
            right = rightC;
            left = leftC;
            data = dataC;
        }
    }

    public class Tree
    {
        public TreeNode root = null;

        bool doesExist(int dataToCheck, TreeNode current)
        {
            if (current != null)
            {
                if (current.data == dataToCheck)
                {
                    return true;
                }
                if (dataToCheck > current.data)
                {
                    return doesExist(dataToCheck, current.right);
                }
                if (dataToCheck < current.data)
                {
                    return doesExist(dataToCheck, current.left);
                }
            }
            return false;
        }

        TreeNode add(int dataToAdd, TreeNode current)
        {
            if (current == null)
            {
                return new TreeNode(null, null, dataToAdd);
            }

            if (dataToAdd < current.data)
            {
                current.left = add(dataToAdd, current.left);
            }

            if (dataToAdd > current.data)
            {
                current.right = add(dataToAdd, current.right);
            }
            return current;
        }

        TreeNode remove(int value, TreeNode current)
        {
            if (current == null)
            {
                return null;
            }

            if (value < current.data)
            {
                current.left = remove(value, current.left);
            }

            if (value > current.data)
            {
                current.right = remove(value, current.right);
            }

            if (value == current.data)
            {
                if (current.left == null && current.right == null)
                {
                    current = null;
                    return null;
                }
                if (current.left == null)
                {
                    TreeNode tempRight = current.right;
                    current = null;
                    return tempRight;
                }

                if (current.right == null)
                {
                    TreeNode tempLeft = current.left;
                    current = null;
                    return tempLeft;
                }
                TreeNode swapWith = current.right;

                while (swapWith.left != null)
                {
                    swapWith = swapWith.left;
                }
                current.data = swapWith.data;
                current.right = remove(swapWith.data, current.right);
            }
            return current;
        }

        public void Add(int dataToAdd)
        {
            if (!doesExist(dataToAdd, root))
            {
                root = add(dataToAdd, root);
            }
        }

        public void Remove(int value)
        {
            if (doesExist(value, root))
            {
                root = remove(value, root);
            }
        }

        void printInOrder(TreeNode current)
        {
            if (current != null)
            {
                Console.Write("{0} ", current.data);
                printInOrder(current.left);
                printInOrder(current.right);
            }
        }

        public void Print()
        {
            printInOrder(root);

        }
        public void PrintOddLevels()
        {
            printOddLevels(root, true);
        }

        void printOddLevels(TreeNode current, bool isOdd)
        {
            if (current == null)
            {
                return;
            }

            if (isOdd)
            {
                Console.Write("{0} ",current.data);
            }

            printOddLevels(current.left, !isOdd);
            printOddLevels(current.right, !isOdd);
        }
    }
    class Solution
    {
        static void Main(String[] args)
        {
            int numberOfOps = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse)[0];
            Tree myTree = new Tree();
            for (int i = 0; i < numberOfOps; i++)
            {

                string[] inputString = Console.ReadLine().Trim().Split();
                string command = inputString[0];


                if (inputString.Length > 1)
                {
                    int value = int.Parse(inputString[1]);
                    if (command == "add")
                    {
                        myTree.Add(value);
                    }
                    if (command == "remove")
                    {
                        myTree.Remove(value);
                    }
                }
                else
                {
                    if (command == "print")
                    {
                        myTree.Print();
                    }

                    if (command == "print_odd_layers")
                    {
                        myTree.PrintOddLevels();
                    }

                }
            }
        }
    }
}
