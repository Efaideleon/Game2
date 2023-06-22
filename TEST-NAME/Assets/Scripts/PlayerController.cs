using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] AudioClip cutSound;
    AudioSource playerAudio;
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponentInChildren<Animator>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cotton"))
        {
            playerAnimator.SetBool("Is_cutting_b", true);
            playerAudio.PlayOneShot(cutSound, 1.0f);
        }
        else {
            playerAnimator.SetBool("Is_cutting_b", false);
        }
    }
}
