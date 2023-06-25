using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private ParticleSpawner particleSpawner;
    // Start is called before the first frame update
    void Start()
    {
        //may need to fix
        particleSpawner = FindObjectOfType<ParticleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            particleSpawner.GetParticle();
        }
    }
}
