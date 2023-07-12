using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave
{
    public GameObject WaveObject { get; private set; }
    public EnemyWave Next { get; set; }

    public EnemyWave(GameObject waveObject)
    {
        WaveObject = waveObject;
        Next = null;
    }
}

public class EnemyWaveQueue
{
    private EnemyWave head;
    private EnemyWave tail;

    public void Enqueue(GameObject waveObject)
    {
        EnemyWave newWave = new EnemyWave(waveObject);

        if (head == null)
        {
            head = newWave;
            tail = newWave;
        }
        else
        {
            tail.Next = newWave;
            tail = newWave;
        }
    }

    public GameObject Dequeue()
    {
        if (head == null)
        {
            Debug.LogWarning("Cola Vacía!");
            return null;
        }

        GameObject waveObject = head.WaveObject;
        head = head.Next;

        if (head == null)
        {
            tail = null;
        }

        return waveObject;
    }

    public bool IsEmpty()
    {
        return head == null;
    }
}

