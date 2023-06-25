using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ParticlePool : MonoBehaviour
{
    private ObjectPool<ParticleSystem> _pool;
    private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _pool = GameObject.FindGameObjectWithTag("BatteryParticleSpawner").GetComponent<ParticleSpawner>().pool;

        ParticleSystem.MainModule main = _particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped() 
    {
        _pool.Release(_particleSystem);
    }
}