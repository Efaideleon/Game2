using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolString : MonoBehaviour
{
    private ParticleSpawner particleSpawner;
    private Queue<ParticleSystem> _pool;
    private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        particleSpawner = GameObject.FindWithTag("CottonParticleSpawner").GetComponent<ParticleSpawner>();
        _pool = particleSpawner.pool;

        ParticleSystem.MainModule main = _particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped() 
    {
        particleSpawner.ReturnParticle(_particleSystem);
    }
}
