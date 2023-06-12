using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // The speed at which the player moves
    private CapsuleCollider2D cc; // The rigidbody of the player

    void Start()
    {
        cc = GetComponent<CapsuleCollider2D>(); // Get the rigidbody component
    } 
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input axis
        float y = Input.GetAxis("Vertical"); // Get the vertical input axis

        // move the player only within the screen
        if (transform.position.x < -8.5f && x < 0) x = 0;
        if (transform.position.x > 8.5f && x > 0) x = 0;
        if (transform.position.y < -4.5f && y < 0) y = 0;
        if (transform.position.y > 4.5f && y > 0) y = 0;

        Vector3 move = transform.right * x + transform.up * y; // Calculate the movement vector
        transform.position += move * speed * Time.deltaTime; // Move the player
    }
}