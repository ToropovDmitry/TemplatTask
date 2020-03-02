#pragma once
#include <iostream>
#include <algorithm>

template <class T>
class AvlTree;

template <class T>
class AvlNode 
{
private:
	T data;
	int height;
	AvlNode *left, *right;
	AvlNode(T data)
	{
		this->data = data;
		this->height = 1;
		this->left = nullptr;
		this->right = nullptr;
	}

	int Balance()
	{
		int leftHeight, rightHeight;
		if (this->left != nullptr)
			leftHeight = this->left->height;
		else
			leftHeight = 0;
		if (this->right != nullptr)
			rightHeight = this->right->height;
		else
			rightHeight = 0;
		return rightHeight - leftHeight;
	}

	void makeHeight() 
	{
		int leftHeight, rightHeight;
		if (this->left != nullptr)
			leftHeight = this->left->height;
		else
			leftHeight = 0;
		if (this->right != nullptr)
			rightHeight = this->right->height;
		else
			rightHeight = 0;
		this->height = std::max(leftHeight, rightHeight) + 1;
	}

public:
	friend class AvlTree<T>;
};

template <class T>
class AvlTree
{
private:
	AvlNode<T> *root;
	int count;
	bool change;
	void TurnLeft(AvlNode<T>* &index)
	{
		AvlNode<T> *temp;
		temp = index->left;
		index->left = temp->right;
		temp->right = index;
		index->makeHeight();
		temp->makeHeight();
		index = temp;
	}

	void TurnRight(AvlNode<T>* &index)
	{
		AvlNode<T> *temp;
		temp = index->right;
		index->right = temp->left;
		temp->left = index;
		index->makeHeight();
		temp->makeHeight();
		index = temp;
	}

	void Ballance(AvlNode<T>* &index)
	{
		int oldHeight = index->height;
		index->makeHeight();
		int balance = index->Balance();
		if (balance > 1)
		{
			if (index->right->Balance() < 0)
				this->TurnLeft(index->right);
			this->TurnRight(index);
			if (index->height == oldHeight)
				this->change = false;
		}
		else if (balance < -1)
		{
			if (index->left->Balance() > 0)
				this->TurnRight(index->left);
			this->TurnLeft(index);
			if (index->height == oldHeight)
				this->change = false;
		}
	}

	void Insert(AvlNode<T>* &index, T data)
	{
		if (index == nullptr)
		{
			this->change = true;
			index = new AvlNode<T>(data);
		}
		else
		{
			if (data < index->data)
			{
				this->Insert(index->left, data);
				if (this->change)
					this->Ballance(index);
			}
			else
			{
				this->Insert(index->right, data);
				if (this->change)
					this->Ballance(index);
			}
		}
	}

	void FindToDelete(AvlNode<T>* &replaceable, AvlNode<T>* index, AvlNode<T>* &temp)
	{
		if (replaceable->right != nullptr)
		{
			this->FindToDelete(replaceable->right, index, temp);
			this->Ballance(replaceable);
		}
		else
		{
			temp = replaceable;
			index->data = replaceable->data;
			replaceable = replaceable->left;
		}
	}

	void Delete(AvlNode<T>* &index, T data)
	{
		AvlNode<T>* temp;
		if (index != nullptr)
		{
			if (data < index->data)
			{
				this->Delete(index->left, data);
				this->Ballance(index);
			}
			else if (data > index->data)
			{
				this->Delete(index->right, data);
				this->Ballance(index);
			}
			else
			{
				temp = index;
				if (index->right == nullptr)
					index = index->left;
				else if (index->left == nullptr)
					index = index->right;
				else
					this->FindToDelete(index->left, index, temp);
				delete temp;
			}
		}
	}

	void ListOfLeaves(AvlNode<T>* treeIndex, T* array, int& arrayIndex) 
	{
		if (treeIndex->left != nullptr)
			ListOfLeaves(treeIndex->left, array, arrayIndex);
		array[arrayIndex] = treeIndex->data;
		arrayIndex++;
		if (treeIndex->right != nullptr)
			ListOfLeaves(treeIndex->right, array, arrayIndex);
	}

	bool search(AvlNode<T> *index, T data)
	{
		if (index != nullptr)
		{
			if (data == index->data)
				return true;
			else if (data < index->data)
				return this->search(index->left, data);
			else if (data > index->data)
				return this->search(index->right, data);
		}
		return false;
	}

	void Dispose(AvlNode<T>* index)
	{
		if (index != nullptr) 
		{
			if (index->left != nullptr)
				this->Dispose(index->left);
			if (index->right != nullptr)
				this->Dispose(index->right);
			delete index;
		}
	}
public:
	AvlTree() 
	{
		this->root = nullptr;
		this->change = false;
		this->count = 0;
	}
	AvlTree(T data) 
	{
		this->root = new AvlNode<T>(data);
		this->change = false;
		this->count = 1;
	}
	~AvlTree() 
	{
		this->Dispose(this->root);
	}
	void insert(T data) 
	{
		this->Insert(this->root, data);
		this->count++;
	}
	void remove(T data) 
	{
		this->Delete(this->root, data);
		this->count--;
	}
	bool Find(T data) 
	{
		return this->_contains(this->root, data);
	}
	int getCount() 
	{
		return this->count;
	}
	T* Leaves() 
	{
		T* array = new T[this->count];
		int arrayIndex = 0;
		ListOfLeaves(this->root, array, arrayIndex);
		return array;
	}
};


