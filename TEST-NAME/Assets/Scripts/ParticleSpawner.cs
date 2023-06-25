using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    private int poolSize = 100;
    private int maxPoolSize = 200;
    public Queue<ParticleSystem> pool;
private void Awake()
    {
        pool = new Queue<ParticleSystem>(poolSize);

        // Initialize the pool
        for (int i = 0; i < poolSize; i++)
        {
            ParticleSystem particle = Instantiate(particlePrefab, this.transform);
            particle.gameObject.SetActive(false);
            pool.Enqueue(particle);
        }
    }

    public ParticleSystem GetParticle()
    {
        if (pool.Count > 0)
        {
            ParticleSystem particle = pool.Dequeue();
            particle.gameObject.SetActive(true);
            return particle;
        }
        else
        {
            // Pool is empty. Handle this, for example by returning null or creating a new particle.
            return null;
        }
    }

    public void ReturnParticle(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
        pool.Enqueue(particle);
    }
}
