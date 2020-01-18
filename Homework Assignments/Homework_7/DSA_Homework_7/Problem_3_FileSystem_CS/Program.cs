using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Problem_3_FileSystem_CS
{
    public class FileNode
    {
        private string _fileName;
        private FileNode _parent;
        private SortedDictionary<string, FileNode> childrenDictionary;

        public string FileName
        {
            get => _fileName;
            set => _fileName = value;
        }

        public FileNode Parent
        {
            get => _parent;
            set => _parent = value;
        }

        public FileNode(string name, FileNode parent)
        {
            FileName = name;
            Parent = parent;
            childrenDictionary = new SortedDictionary<string, FileNode>();
        }

        public FileNode()
        {
            FileName = " ";
            Parent = null;
            childrenDictionary = new SortedDictionary<string, FileNode>();
        }

        public bool dirExists(string dirName)
        {
            if (childrenDictionary.ContainsKey(dirName))
            {
                return true;
            }

            return false;
        }

        void addChild(string dirName, FileNode parentNode)
        {
            FileNode newFile = new FileNode(dirName, parentNode);
            childrenDictionary.Add(dirName, newFile);
        }

        public void MakeDir(string dirName, FileNode parentNode)
        {
            if (dirExists(dirName))
            {
                Console.Write("Directory already exists\n");
                return;
            }
            addChild(dirName, parentNode);
        }

        public FileNode GetDir(string dirName)
        {
            FileNode result;
            childrenDictionary.TryGetValue(dirName, out result);
            return result;
        }

        public void PrintChildren()
        {
            foreach (var fileNode in childrenDictionary)
            {
                Console.Write("{0} ", fileNode.Key);
            }
            Console.WriteLine();
        }
    };

    public class FileTree
    {
        private FileNode root;
        private FileNode currentDir;

        public FileTree()
        {
            root = new FileNode("/", null);
            currentDir = root;
        }

        public void AddDir(string dirName)
        {
            currentDir.MakeDir(dirName, currentDir);
        }

        public void ChangeDir(string dirName)
        {
            if (dirName != "..")
            {
                if (currentDir.dirExists(dirName))
                {
                    currentDir = currentDir.GetDir(dirName);
                    return;
                }

                Console.Write("No such directory\n");
                return;
            }
            if (currentDir.Parent != null)
            {
                currentDir = currentDir.Parent;
                return;
            }
            Console.Write("No such directory\n");
        }

        public void PrintChildren()
        {
            currentDir.PrintChildren();
        }

        public void PrintFullPath()
        {
            if (currentDir.FileName == "/")
            {
                Console.WriteLine("/");
                return;
            }
            FileNode tempCurrent = currentDir;
            List<string> path = new List<string>();
            while (tempCurrent.Parent != null)
            {
                path.Add(tempCurrent.FileName);
                tempCurrent = tempCurrent.Parent;
            }

            for (int i = path.Count-1; i >= 0; i--)
            {
                Console.Write("/{0}", path[i]);
            }
            Console.WriteLine("/");

        }
    };

    class Program
    {
        static void Main(string[] args)
        {
            int numberOfOps;
            string[] input = Console.ReadLine().Trim(' ').Split(' ');
            numberOfOps = Array.ConvertAll(input, int.Parse)[0];
            string[] op;
            FileTree treeBoi = new FileTree();
            for (int i = 0; i < numberOfOps; i++)
            {
                op = Console.ReadLine().Trim(' ').Split(' ');

                 switch (op[0])
                {
                    case "mkdir":
                        treeBoi.AddDir(op[1]);
                        break;

                    case "cd":
                        treeBoi.ChangeDir(op[1]); 
                        break;

                    case "pwd":
                        treeBoi.PrintFullPath();
                        break;

                    case "ls":
                        treeBoi.PrintChildren();
                        break;
                }

            }
        }
    }
}
