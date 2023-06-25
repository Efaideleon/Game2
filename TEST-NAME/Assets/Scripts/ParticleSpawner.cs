using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    private int initPoolSize = 100;
    private int maxPoolSize = 200;
    public ObjectPool<ParticleSystem> pool;

    private void Awake()
    {

        pool = new ObjectPool<ParticleSystem>(
                    // Action when creating new instance
                    () => Instantiate(particlePrefab),
                    // Action when enabling an instance
                    particle => { particle.gameObject.SetActive(true); particle.Play(true); },
                    // Action when disabling an instance
                    particle => { particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); particle.gameObject.SetActive(false); },
                    // Action when destroying an instance
                    particle => { Destroy(particle.gameObject); },
                    // Should the pool's action be run in the Unity runtime context
                    true,
                    // Initial pool size
                    100,
                    // Should the pool automatically expand
                    200        
        );
        // pool = new ObjectPool<ParticleSystem>(
        //     () => Instantiate(particlePrefab),
        //     particle => { particle.gameObject.SetActive(true); },
        //     particle => { particle.gameObject.SetActive(false); },
        //     particle => { Destroy(particle.gameObject); },
        //     true,
        //     initPoolSize,
        //     maxPoolSize
        // );

        Instantiate(particlePrefab, transform);
        Debug.Log("ParticleSpawner Awake");
    }

    public ParticleSystem GetParticle()
    {
        return pool.Get();
    }

    public void ReturnParticle(ParticleSystem particle)
    {
        pool.Release(particle);
    }
}
