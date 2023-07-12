using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector3 SpawnReferencePosition;
    public Quaternion spawnRotation;
    public int amounToSpawn;    
    public float spawnCadence;
    public float initialWaitTime;
    private void Start()
    {
        StartCoroutine(EnemySpawnerCoroutine());
    }
    private IEnumerator EnemySpawnerCoroutine()
    {
        yield return new WaitForSeconds(initialWaitTime);
        for (int i = 0; i < amounToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-SpawnReferencePosition.x, SpawnReferencePosition.x), SpawnReferencePosition.y, SpawnReferencePosition.z);
            SpawnEnemy(randomPosition, spawnRotation);
            yield return new WaitForSeconds(spawnCadence);

        }
    }
    public void SpawnEnemy(Vector3 enemyPosition, Quaternion rotation)
    {
        Instantiate(enemyPrefab, enemyPosition, rotation);        
    }
}
