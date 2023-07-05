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
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamController : MonoBehaviour
{
    [SerializeField] private List<EnemyWaveConfig> wavesConfig;
    [SerializeField] private Quaternion spawnRotation;
    [SerializeField] private float initialWaitTime = 2f;

    private void Start()
    {
        StartCoroutine(EnemySpawnerCoroutine());
    }

    private IEnumerator EnemySpawnerCoroutine()
    {
        yield return new WaitForSeconds(initialWaitTime);

        foreach (EnemyWaveConfig waveConfig in wavesConfig)
        {
            foreach (EnemyConfig enemy in waveConfig.enemies)
            {
                Vector3 enemyPosition = Vector3.zero;

                if (enemy.useSpecificPosition)
                {
                    enemyPosition = enemy.spawnReferencePosition;
                }
                else
                {
                    enemyPosition.x = Random.Range(-enemy.spawnPositionDifference.x, enemy.spawnPositionDifference.x);
                    enemyPosition.y = Random.Range(-enemy.spawnPositionDifference.y, enemy.spawnPositionDifference.y);
                    enemyPosition.z = enemy.spawnPositionDifference.z;
                }

                Quaternion spawnRotation = Quaternion.Euler(0f, 0f, enemy.spawnRotation);

                SpawnEnemy(enemy.enemyPrefab.gameObject, enemyPosition, spawnRotation);

                yield return new WaitForSeconds(waveConfig.spawnCadence);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, Vector3 enemyPosition, Quaternion rotation)
    {
        Instantiate(enemyPrefab, enemyPosition, rotation);
    }
}
*/