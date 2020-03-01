// TemplatTask.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "Stack.h"

int main()
{
	Stack<int> stack1 = Stack<int>(3);
	stack1.push(1);
	stack1.push(2);
	stack1.push(3);	
	Stack<int> stack2 = Stack<int>(2);
	stack2.push(4);
	stack2.push(5);
	Stack<int> stack = StackMerge(stack1, stack2);
	for (int i = 0; i < 5; i++)
		std::cout << stack.pop() << std::endl;	
	/*Stack<int> stack1 = Stack<int>(2);
	Stack<int> stack2 = Stack<int>(2);
	stack1.push(1);
	stack1.push(2);
	stack2.push(3);
	Stack<int> mergeStack = StackMerge(stack1, stack2);
	using namespace std;
	cout << mergeStack.pop() << endl;
	cout << mergeStack.pop() << endl;
	cout << mergeStack.pop() << endl;
	cout << mergeStack.pop() << endl;*/
    return 0;
}

