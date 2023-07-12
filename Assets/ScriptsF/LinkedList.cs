using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LinkedList<T>
{
    private Node<T> head;
    private int count;

    public LinkedList()
    {
        head = null;
        count = 0;
    }

    public void Add(T item)
    {
        Node<T> newNode = new Node<T>(item);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }

        count++;
    }

    public T this[int index]
    {
        get
        {
            Node<T> current = head;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex == index)
                {
                    return current.Data;
                }

                current = current.Next;
                currentIndex++;
            }

            throw new System.IndexOutOfRangeException();
        }
    }

    public int Count
    {
        get { return count; }
    }
}

 class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }

    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}
