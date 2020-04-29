using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            AvlTree<int> tree = new AvlTree<int>();
            tree.Insert(60);
            tree.Insert(80);
            tree.Insert(90);
            tree.Insert(100);
            tree.Insert(130);
            tree.Remove(90);
            bool search1 = tree.Find(120);
            bool search2 = tree.Find(100);
            bool search3 = tree.Find(140);
            Console.Write("Tree have 120 - " + search1.ToString());
            Console.WriteLine();
            Console.Write("Tree have 100 - " + search2.ToString());
            Console.WriteLine();
            Console.Write("Tree have 140 - " + search3.ToString());
            Console.WriteLine();
            int[] arr = tree.Leaves();
            for (int i = 0; i < tree.getCount(); i++)
                Console.Write("{0} ", arr[i]);
            Console.WriteLine();            
        }
    }
}
