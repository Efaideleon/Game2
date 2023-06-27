using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public abstract class ParticlePool : MonoBehaviour
{
    private ParticleSpawner particleSpawner;
    private Queue<ParticleSystem> _pool;
    private ParticleSystem _particleSystem;
    private string particleSpawnerTag;
    // Start is called before the first frame update
    void Start()
    {
        SetPool();
        SetStopActionToCallback();
    }

    private void OnParticleSystemStopped() 
    {
        particleSpawner.ReturnParticle(_particleSystem);
    }

    public void SetParticleSapwnerTag(string tag)
    {
        particleSpawnerTag = tag;
    }

    protected abstract void SetParticleSpawner();

    private void SetPool()
    {
        SetParticleSpawner();
        particleSpawner = GameObject.FindWithTag(particleSpawnerTag).GetComponent<ParticleSpawner>();
        _particleSystem = GetComponent<ParticleSystem>();
        _pool = particleSpawner.pool;
    }

    private void SetStopActionToCallback()
    {
        ParticleSystem.MainModule main = _particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
}