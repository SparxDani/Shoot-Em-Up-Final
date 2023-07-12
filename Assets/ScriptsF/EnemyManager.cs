using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float spawnInterval = 5f;

    private EnemyWaveQueue waveQueue;
    private float timer = 0f;

    void Start()
    {
        waveQueue = new EnemyWaveQueue();

        GameObject wave1 = new GameObject("Wave1");
        wave1.SetActive(false);
        waveQueue.Enqueue(wave1);

        GameObject wave2 = new GameObject("Wave2");
        wave1.SetActive(false);
        waveQueue.Enqueue(wave1);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && !waveQueue.IsEmpty())
        {
            GameObject nextWave = waveQueue.Dequeue();
            nextWave.SetActive(true);
            timer = 0f;
            print(nextWave.name);
        }
    }
}
