using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplyLinkedList<T>
{
    class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    private Node Head { get; set; }
    public int Count { get; private set; }

    public void AddNodeAtStart(T value)
    {
        Node newNode = new Node(value);
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            Node tmp = Head;
            Head = newNode;
            Head.Next = tmp;
        }
        Count++;
    }

    public void AddNodeAtEnd(T value)
    {
        if (Head == null)
        {
            AddNodeAtStart(value);
        }
        else
        {
            Node tmp = Head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            Node newNode = new Node(value);
            tmp.Next = newNode;
        }
        Count++;
    }

    public void AddNodeAtPosition(T value, int position)
    {
        if (position == 0)
        {
            AddNodeAtStart(value);
        }
        else if (position == Count)
        {
            AddNodeAtEnd(value);
        }
        else if (position > Count || position < 0)
        {
            Debug.Log("No se puede hacer esta operacion");
        }
        else
        {
            int currentPosition = 0;
            Node tmp = Head;
            while (currentPosition < position - 1)
            {
                tmp = tmp.Next;
                currentPosition++;
            }
            Node nodeAtPosition = tmp.Next;
            Node newNode = new Node(value);
            tmp.Next = newNode;
            newNode.Next = nodeAtPosition;
            Count++;
        }
    }

    public void ModifyAtStart(T value)
    {
        if (Head == null)
        {
            Debug.Log("No se puede hacer esta operacion");
        }
        else
        {
            Head.Value = value;
        }
    }

    public void ModifyAtEnd(T value)
    {
        if (Head == null)
        {
            Debug.Log("No se puede hacer esta operacion");
        }
        else
        {
            Node tmp = Head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            tmp.Value = value;
        }
    }

    public void ModifyAtPosition(T value, int position)
    {
        if (position == 0)
        {
            ModifyAtStart(value);
        }
        else if (position == Count - 1)
        {
            ModifyAtEnd(value);
        }
        else if (position > Count || position < 0)
        {
            Debug.Log("No se puede hacer esta operacion");
        }
        else
        {
            Node tmp = Head;
            int currentPosition = 0;
            while (currentPosition < position)
            {
                tmp = tmp.Next;
                currentPosition++;
            }
            tmp.Value = value;
        }
    }

    public T GetNodeAtStart()
    {
        if (Head == null)
        {
            return default(T);
        }
        else
        {
            return Head.Value;
        }
    }

    public T GetNodeAtEnd()
    {
        if (Head == null)
        {
            return default(T);
        }
        else
        {
            Node tmp = Head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            return tmp.Value;
        }
    }

    public T GetNodeAtPosition(int position)
    {
        if (position == 0)
        {
            return GetNodeAtStart();
        }
        else if (position == Count - 1)
        {
            return GetNodeAtEnd();
        }
        else if (position >= Count || position < 0)
        {
            return default(T);
        }
        else
        {
            int iterator = 0;
            Node tmp = Head;
            while (iterator < position)
            {
                tmp = tmp.Next;
                iterator++;
            }
            return tmp.Value;
        }
    }

    public void RemoveAtStart()
    {
        if (Head == null)
        {
            Debug.Log("Eso no se puede hacer");
        }
        else
        {
            Node newHead = Head.Next;
            Head.Next = null;
            Head = newHead;
            Count--;
        }
    }

    public void RemoveAtEnd()
    {
        if (Head == null)
        {
            Debug.Log("Eso no se puede hacer");
        }
        else if (Count == 1)
        {
            RemoveAtStart();
        }
        else
        {
            Node tmp = Head;
            while (tmp.Next.Next != null)
            {
                tmp = tmp.Next;
            }
            tmp.Next = null;
            Count--;
        }
    }

    public void RemoveNodeAtPosition(int position)
    {
        if (position == 0)
        {
            RemoveAtStart();
        }
        else if (position == Count - 1)
        {
            RemoveAtEnd();
        }
        else if (position >= Count || position < 0)
        {
            Debug.Log("Eso no se puede hacer");
        }
        else
        {
            int iterator = 0;
            Node previousNode = Head;
            while (iterator < position - 1)
            {
                previousNode = previousNode.Next;
                iterator++;
            }
            Node currentNode = previousNode.Next;
            Node nextNode = currentNode.Next;
            currentNode.Next = null;
            previousNode.Next = nextNode;
            Count--;
        }
    }

    public void PrintAllNodes()
    {
        Node tmp = Head;
        while (tmp != null)
        {
            Debug.Log(tmp.Value);
            tmp = tmp.Next;
        }
    }
}
