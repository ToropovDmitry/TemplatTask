using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Stack<T> : ICloneable
    {        
        T[] array;
        public int size;
        public int head;
        bool Empty()
        {
            if (head == size)
                return true;
            else
                return false;
        }
        bool Full()
        {
            if (head == 0)
                return true;
            else
                return false;
        }
        public Stack(int size)
        {
            this.size = size;
            this.array = new T[this.size];
            this.head = size;
        }
        public void Push(T element)
        {
            if (this.Full())
                throw new InvalidOperationException("Stack is full!");
            this.head = this.head - 1;
            this.array[this.head] = element;
        }
        public T Pop()
        {
            if (this.Empty())
                throw new InvalidOperationException("Stack is empty!");
            T element = this.array[this.head];            
            this.head = this.head + 1;
            return element;
        }        
        public static Stack<T> StackMerge(Stack<T> first, Stack<T> second)
        {
            int size = first.size + second.size;
            Stack<T> stack = new Stack<T>(size);
            int index = first.size;
            while (index != first.head)
            {
                index = index - 1;
                stack.Push(first.array[index]);
            }
            index = second.size;
            while (index != second.head)
            {
                index = index - 1;
                stack.Push(second.array[index]);
            }
            return stack;
        }
        public object Clone()
        {
            Stack<T> stack = new Stack<T>(this.array.Length);
            stack.array = (T[])this.array.Clone();
            stack.head = this.head;
            stack.size = this.size;
            return stack;
        }
    }
}
