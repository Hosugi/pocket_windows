#include <iostream>
#include <cstdlib>
using namespace std;
typedef struct node node_t;

typedef struct node {
	int tag;
	int value;
	// 값
	node_t* next;
	// 다음 노드 주소를 담을 ptr
}node_t;

void add(node_t** head, int value)					//구조체 node_t를 참조하는 포인터'의' 주소, 일반 값
{

	node_t* new_node = (node_t*)malloc(sizeof(node_t));
	new_node->value = value;
	new_node->next = *head;
	*head = new_node;
}
void print(node_t* head)
{
	node_t* now = head;
	for (; now != NULL; now = now->next)
	{
		cout << now->value << "->";
	}
	cout << "NULL" << endl;
}
void destory(node_t** head)
{
	node_t* now = *head;
	node_t* tmp = (now)->next;
	for (;now != NULL;)
	{
		tmp = (now)->next;
		free(now);
		now = tmp;
	}
	*head = NULL;
}
int main()
{
	node_t* head = NULL;
	add(&head, 1);
	print(head);
	add(&head, 3);
	print(head);
	destory(&head);
	print(head);
}
