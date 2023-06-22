using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] AudioClip cutSound;
    public bool isCutting = false;
    AudioSource playerAudio;
    PlayerMovement playerMovementScript;
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        if (playerMovementScript.moving)
        {
            playerAnimator.SetBool("moving_b", true);
        }
        else
        {
            playerAnimator.SetBool("moving_b", false);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cotton"))
        {
            playerAudio.PlayOneShot(cutSound, 1.0f);
            playerAnimator.SetTrigger("cutting_t");
        }
    }
}
