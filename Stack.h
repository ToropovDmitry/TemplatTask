#pragma once
#include <iostream>
#include <stdexcept>

template <class T> 
class Stack;
template<class T>
Stack<T> StackMerge(const Stack<T>& first, const Stack<T>& second);

template<class T> 
class Stack
{
private:
	int size;
	T *array;
	int head;
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
public:
	Stack(int size)
	{
		this->size = size;
		this->array = new T[this->size];
		this->head = size;
	}
	Stack(const Stack& stack)
	{
		this->size = stack.size;
		this->array = new T[this->size];
		this->head = stack.head;
	}

	~Stack()
	{
		delete[] array;
	}

	void push(T element)
	{
		if (this->Full())
			throw std::out_of_range("Stack is full!");
		this->head = this->head - 1;
		this->array[this->head] = element;
	}

	T pop()
	{
		if (this->Empty())
			throw std::out_of_range("Stack is empty!");
		T element = this->array[this->head];
		this->head = this->head + 1;
		return element;
	}
	friend Stack StackMerge<T>(const Stack<T>& first, const Stack<T>& second);
};

template<class T> 
Stack<T> StackMerge(const Stack<T>& first, const Stack<T>& second)
{
	int size = first.size + second.size;
	Stack<T> stack = Stack<T>(size);
	int index = first.size;	
	while (index != first.head) 
	{
		index = index - 1;
		stack.push(first.array[index]);					
	}
	index = second.size;
	while (index != second.head) 
	{
		index = index - 1;
		stack.push(second.array[index]);		
	}
	return stack;
}

