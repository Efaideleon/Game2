using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private ParticleSpawner particleSpawner;
    private GameManager gameManager; 
    // Start is called before the first frame update
    void Start()
    {
        //may need to fix
        particleSpawner = GameObject.FindGameObjectWithTag("BatteryParticleSpawner").GetComponent<ParticleSpawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateScore(5);
            gameObject.SetActive(false);
            ParticleSystem particle = particleSpawner.GetParticle();
            particle.transform.position = transform.position;
            particle.Play();
        }
    }
}
