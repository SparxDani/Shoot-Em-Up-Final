using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva ola de enemigos", menuName = "Enemigos/Configuracion de oleada")]
public class WaveEnemySO : ScriptableObject
{
    public EachEnemyConfig Config;
    public class EachEnemyConfig
    {
        //public EnemyController enemyPrefab;
        public Vector3 spawnReferencePosition;
        public bool useSpecificPosition;
    }
    public SimpleList<EachEnemyConfig> enemies;
    public float cadence;
}
