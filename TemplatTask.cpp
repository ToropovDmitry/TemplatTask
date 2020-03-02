// TemplatTask.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "AvlTree.h"

int main()
{
	AvlTree<int> tree = AvlTree<int>(60);	
	tree.insert(70);
	tree.insert(80);
	tree.insert(90);
	tree.insert(100);
	tree.insert(130);
	int* arr = tree.Leaves();
	for (int i = 0; i < tree.getCount(); i++)
		std::cout << arr[i] << " ";
	std::cout << std::endl;
	tree.remove(90);
	arr = tree.Leaves();
	for (int i = 0; i < tree.getCount(); i++)
		std::cout << arr[i] << " ";
	delete[] arr;
    return 0;
}

