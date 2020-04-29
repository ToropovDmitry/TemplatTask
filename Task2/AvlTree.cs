using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class AvlTree<T> where T : IComparable
    {
        class AvlNode
        {
            public T data;
            public int height;
            public AvlNode left, right;            
            public AvlNode(T data)
            {
                this.data = data;
                this.height = 1;
                this.left = null;
                this.right = null;
            }

            public int Balance()
            {
                int leftHeight, rightHeight;
                if (this.left != null)
                    leftHeight = this.left.height;
                else
                    leftHeight = 0;
                if (this.right != null)
                    rightHeight = this.right.height;
                else
                    rightHeight = 0;
                return rightHeight - leftHeight;
            }


            public void MakeHeight()
            {
                int leftHeight, rightHeight;
                if (this.left != null)
                    leftHeight = this.left.height;
                else
                    leftHeight = 0;
                if (this.right != null)
                    rightHeight = this.right.height;
                else
                    rightHeight = 0;
                this.height = Math.Max(leftHeight, rightHeight) + 1;
            }
        }

        AvlNode root;
        int count;
        bool change;

        public AvlTree()
        {
            this.root = null;
            this.change = false;
            this.count = 0;
        }

        public AvlTree(T data)
        {
            this.root = new AvlNode(data);
            this.change = false;
            this.count = 1;
        }

        void TurnLeft(ref AvlNode index)
        {
            AvlNode temp = index.left;
            index.left = temp.right;
            temp.right = index;
            index.MakeHeight();
            temp.MakeHeight();
            index = temp;
        }

        void TurnRight(ref AvlNode index)
        {
            AvlNode temp;
            temp = index.right;
            index.right = temp.left;
            temp.left = index;
            index.MakeHeight();
            temp.MakeHeight();
            index = temp;
        }

        void Ballance(ref AvlNode index)
        {
            int oldHeight = index.height;
            index.MakeHeight();
            int balance = index.Balance();
            if (balance > 1)
            {
                if (index.right.Balance() < 0)
                    this.TurnLeft(ref index.right);
                this.TurnRight(ref index);
                if (index.height == oldHeight)
                    this.change = false;
            }
            else if (balance < -1)
            {
                if (index.left.Balance() > 0)
                    this.TurnRight(ref index.left);
                this.TurnLeft(ref index);
                if (index.height == oldHeight)
                    this.change = false;
            }
        }

        void Insert(ref AvlNode index, T data)
        {
            if (index == null)
            {
                this.change = true;
                index = new AvlNode(data);
            }
            else
            {
                if (data.CompareTo(index.data) == -1)
                {
                    this.Insert(ref index.left, data);
                    if (this.change)
                        this.Ballance(ref index);
                }
                else
                {
                    this.Insert(ref index.right, data);
                    if (this.change)
                        this.Ballance(ref index);
                }
            }
        }

        void FindToDelete(ref AvlNode replaceable, AvlNode index, ref AvlNode temp)
        {
            if (replaceable.right != null)
            {
                this.FindToDelete(ref replaceable.right, index, ref temp);
                this.Ballance(ref replaceable);
            }
            else
            {
                temp = replaceable;
                index.data = replaceable.data;
                replaceable = replaceable.left;
            }
        }

        void Delete(ref AvlNode index, T data)
        {             
            if (index != null)
            {
                if (data.CompareTo(index.data) == -1)
                {
                    this.Delete(ref index.left, data);
                    this.Ballance(ref index);
                }
                else if (data.CompareTo(index.data) == 1)
                {
                    this.Delete(ref index.right, data);
                    this.Ballance(ref index);
                }
                else
                {
                    AvlNode temp = index;
                    if (index.right == null)
                        index = index.left;
                    else if (index.left == null)
                        index = index.right;
                    else
                        this.FindToDelete(ref index.left, index, ref temp);                   
                }
            }
        }

        void ListOfLeaves(AvlNode treeIndex, T[] array, ref int arrayIndex)
        {
            if (treeIndex.left != null)
                ListOfLeaves(treeIndex.left, array, ref arrayIndex);
            array[arrayIndex] = treeIndex.data;
            arrayIndex++;
            if (treeIndex.right != null)
                ListOfLeaves(treeIndex.right, array, ref arrayIndex);
        }

        bool Search(AvlNode index, T data)
        {
            if (index != null)
            {
                if (data.CompareTo(index.data) == 0)
                    return true;
                else if (data.CompareTo(index.data) == -1)
                    return this.Search(index.left, data);
                else if (data.CompareTo(index.data) == 1)
                    return this.Search(index.right, data);
            }
            return false;
        }

        void Dispose(AvlNode index)
        {
            if (index != null)
            {
                if (index.left != null)
                    this.Dispose(index.left);
                if (index.right != null)
                    this.Dispose(index.right);                
            }
        }

       public void Insert(T data)
        {
            this.Insert(ref this.root, data);
            this.count++;
        }
        public void Remove(T data)
        {
            this.Delete(ref this.root, data);
            this.count--;
        }

        bool _contains(AvlNode pointer, T data)
        {
            if (pointer != null)
            {
                if (data.CompareTo(pointer.data) == -1)
                    return this._contains(pointer.left, data);
                else if (data.CompareTo(pointer.data) == 1)
                    return this._contains(pointer.right, data);
                else
                    return true;
            }
            return false;
        }

        public bool Find(T data)
        {
            return this._contains(this.root, data);
        }
        public int getCount()
        {
            return this.count;
        }

        public T[] Leaves()
        {
            T[] array = new T[this.count];
            int arrayIndex = 0;
            ListOfLeaves(this.root, array, ref arrayIndex);
            return array;
        }
    }
}  