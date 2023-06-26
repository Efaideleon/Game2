using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject needleEnemyPrefab;
    private int numOfNeedleEnemiesToSpawn = 1;
    private int needleEnemyStartX = -35;
    private int needleEnemyStartY = 24;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNeedleEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnNeedleEnemy()
    {
        Instantiate(
            needleEnemyPrefab,
            new Vector3(needleEnemyStartX, needleEnemyStartY, 0),
            needleEnemyPrefab.transform.rotation
        );
    }

    public void SpawnNeedleEnemies()
    {
        for (int i = 0; i < numOfNeedleEnemiesToSpawn; i++)
        {
            SpawnNeedleEnemy();
        }
    }
}
