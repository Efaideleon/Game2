using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
public class NeedleEnemy : MonoBehaviour
{
    private GameManager gameManager;
    private NavMeshAgent needleEnemyNavMeshAgent;
    private Pool enemySpawner;
    private Pool VFXSpawner;
    private VisualEffect needleEnemyDeathVFX;
    private int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        needleEnemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemySpawner = GameObject.FindWithTag("NeedleEnemySpawner").GetComponent<Pool>();
        VFXSpawner = GameObject.FindWithTag("VFXSpawner").GetComponent<Pool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        }
    }

    public void ResetHealth()
    {
        health = 5;
    }

    public void KillEnemy()
    {
        health--;
        
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        needleEnemyDeathVFX = VFXSpawner.GetObject().GetComponent<VisualEffect>();
        needleEnemyDeathVFX.transform.position = transform.position;
        needleEnemyDeathVFX.Play();
        gameManager.UpdateScore(10);
        gameManager.UpdateNumOfEnemiesOnScreen(-1);
        gameObject.SetActive(false);
        needleEnemyNavMeshAgent.enabled = false;
        enemySpawner.ReturnObject(gameObject);
    } 
}
