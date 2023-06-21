using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioClip cutSound;
    AudioSource playerAudio;
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cotton"))
        {
            playerAudio.PlayOneShot(cutSound, 1.0f);
        }
    }
}
