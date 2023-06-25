using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyYarnCube : MonoBehaviour
{
    [SerializeField] ParticleSystem yarnCubeExplosion;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(yarnCubeExplosion, transform.position, yarnCubeExplosion.transform.rotation);
            Destroy(gameObject);
        }
    }
}
