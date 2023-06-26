using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ParticlePool : MonoBehaviour
{
    private ParticleSpawner particleSpawner;
    private Queue<ParticleSystem> _pool;
    private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSpawner = GameObject.FindWithTag("BatteryParticleSpawner").GetComponent<ParticleSpawner>();
        _particleSystem = GetComponent<ParticleSystem>();
        _pool = particleSpawner.pool;

        ParticleSystem.MainModule main = _particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped() 
    {
        particleSpawner.ReturnParticle(_particleSystem);
    }
}