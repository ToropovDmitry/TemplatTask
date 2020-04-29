using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack1 = new Stack<int>(5);
            Stack<int> stack2 = new Stack<int>(3);
            stack1.Push(1);
            stack1.Push(2);
            stack1.Push(3);
            stack1.Push(4);
            stack1.Push(5);            
            stack2.Push(6);
            stack2.Push(7);
            stack2.Push(8);            
            Console.WriteLine(stack1.Pop());
            Console.WriteLine(stack1.Pop());           
            Stack<int> st = Stack<int>.StackMerge(stack1, stack2);          
            for (int i = st.head; i < st.size; i++)
                Console.WriteLine(st.Pop());
        }
    }
}
