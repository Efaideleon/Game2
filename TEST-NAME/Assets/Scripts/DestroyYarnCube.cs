using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyYarnCube : MonoBehaviour
{
    [SerializeField] ParticleSystem yarnCubeExplosion;
    private Pool particleSpawner;
    private EnemyNeedleMovementAI patrolAgent;
    void Start()
    {
        //may need to fix
        particleSpawner = GameObject.FindWithTag("CottonParticleSpawner").GetComponent<Pool>();
        patrolAgent = GameObject.FindWithTag("Enemy").GetComponent<EnemyNeedleMovementAI>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ParticleSystem particle = particleSpawner.GetObject().GetComponent<ParticleSystem>();
            particle.transform.position = transform.position;
            particle.Play();
            //gameObject should be set inactive instead of destroyed
            gameObject.SetActive(false);
            patrolAgent.UnblockWaypoint(transform.position);
        }
    }
}
