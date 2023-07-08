using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Array de prefabs de enemigos
    public Transform[] spawnPoints;   // Array de puntos de generación de enemigos

    public float spawnRate = 1f;       // Tasa de generación de enemigos (enemigos por segundo)

    private float nextSpawnTime;       // Tiempo en el que se generará el próximo enemigo

    private void Start()
    {
        nextSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        // Selecciona un prefab de enemigo aleatoriamente
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Selecciona un punto de generación aleatoriamente
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instancia el enemigo en el punto de generación seleccionado
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
