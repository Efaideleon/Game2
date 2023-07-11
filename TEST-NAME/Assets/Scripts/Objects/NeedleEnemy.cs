using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NeedleEnemy : MonoBehaviour
{
    private GameManager gameManager;
    private NavMeshAgent needleEnemyNavMeshAgent;
    private Pool enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        needleEnemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemySpawner = GameObject.FindWithTag("NeedleEnemySpawner").GetComponent<Pool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateScore(10);
            gameManager.UpdateNumOfEnemiesOnScreen(-1);
            gameObject.SetActive(false);
            needleEnemyNavMeshAgent.enabled = false;
            enemySpawner.ReturnObject(gameObject);
        }
    }
}
